"""Validate the documentation bundle, optionally against a framework source checkout."""
from __future__ import annotations

import argparse
import hashlib
import json
import os
from pathlib import Path
import re
from urllib.parse import unquote


def without_fences(text: str) -> str:
    return re.sub(r"^```[^\n]*\n.*?^```\s*$", "", text, flags=re.M | re.S)


def anchors(path: Path) -> set[str]:
    result: set[str] = set()
    duplicates: dict[str, int] = {}
    for heading in re.findall(r"^#{1,6}\s+(.+?)\s*#*\s*$", without_fences(path.read_text(encoding="utf-8")), re.M):
        slug = re.sub(r"[^\w\- ]", "", heading.lower()).replace(" ", "-")
        count = duplicates.get(slug, 0)
        duplicates[slug] = count + 1
        result.add(slug + (f"-{count}" if count else ""))
    return result


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--source-root", type=Path, help="Framework checkout, distinct from this documentation checkout")
    args = parser.parse_args()
    bundle = Path(__file__).resolve().parents[2]
    candidate = args.source_root or (Path(os.environ["OBFRAMEWORK_SOURCE_ROOT"]) if os.environ.get("OBFRAMEWORK_SOURCE_ROOT") else None)
    if candidate is None and (bundle.parent / "Framework/ObFramework.csproj").exists():
        candidate = bundle.parent
    source = candidate.resolve() if candidate else None
    if source is not None and not (source / "Framework/ObFramework.csproj").is_file():
        print("ERROR: --source-root must point to a framework source checkout containing Framework/ObFramework.csproj")
        return 1

    failures: list[str] = []
    count = 0
    excluded = {".git", "artifacts", "obj", "bin", "__pycache__"}
    pages = [page for page in sorted(bundle.rglob("*.md")) if not excluded & set(page.relative_to(bundle).parts)]
    for page in pages:
        label = page.relative_to(bundle)
        raw = page.read_text(encoding="utf-8")
        public = page.is_relative_to(bundle / "docs")
        if public and re.search(r"ObFramework\.Test|\bNUnit\b|\bdotnet\s+test\b", raw, re.I):
            failures.append(f"{label}: test dependencies belong outside public docs/")
        for target in re.findall(r"\[[^\]\n]*\]\(([^)\n]+)\)", without_fences(raw)):
            target = target.strip().strip("<>")
            if re.match(r"[A-Za-z][\w+.-]*:", target):
                continue
            path, _, anchor = unquote(target).partition("#")
            actual = (page.parent / path).resolve() if path else page
            count += 1
            if not actual.is_relative_to(bundle):
                failures.append(f"{label}: source links must be public URLs or source-path text, not checkout-relative links: {target}")
            elif not actual.exists():
                failures.append(f"{label}: missing {target}")
            elif anchor and actual.suffix == ".md" and anchor not in anchors(actual):
                failures.append(f"{label}: missing heading {target}")

    inventory_path = bundle / "docs/api-inventory.json"
    if not inventory_path.is_file():
        failures.append("Missing docs/api-inventory.json")
        projects = []
    else:
        projects = json.loads(inventory_path.read_text(encoding="utf-8"))
    for project in projects:
        if project["project"] == "ObFramework.Test":
            failures.append("The public API inventory must exclude the testing project")
        if not (bundle / "docs" / project["project"] / "README.md").is_file():
            failures.append(f"Missing guide: {project['project']}")
        if source is None:
            continue
        recorded = set()
        for entry in project["files"]:
            path = (source / entry["source"]).resolve()
            if not path.is_relative_to(source):
                failures.append(f"Source path escapes checkout: {entry['source']}")
                continue
            recorded.add(path)
            if not path.is_file():
                failures.append(f"Removed source: {entry['source']}")
            elif hashlib.sha256(path.read_bytes()).hexdigest() != entry["sha256"]:
                failures.append(f"Changed source; regenerate API: {entry['source']}")
        source_root = (source / project["sourceRoot"]).resolve()
        if not source_root.is_relative_to(source):
            failures.append("Source inventory root escapes checkout")
            continue
        discovered = {path.resolve() for path in source_root.rglob("*.cs") if not excluded & set(path.relative_to(source_root).parts)}
        for path in sorted(discovered - recorded):
            failures.append(f"New source missing from inventory: {path.relative_to(source)}")

    for failure in failures:
        print("ERROR: " + failure)
    print(f"Checked {count} local links across {len(pages)} Markdown files: {len(failures)} error(s).")
    print("Source verification: " + ("checked against supplied/discovered checkout." if source else "skipped (no framework source checkout; bundle-only validation)."))
    return 1 if failures else 0


if __name__ == "__main__":
    raise SystemExit(main())
