# Publication review

The documentation/AI bundle was reviewed before extraction into OBSERVATER/ObFramework.Docs.

## Findings and repairs

- The old skill conflated the documentation root and source root. Canonical instructions and tooling now distinguish them; standalone checks report skipped source verification, while mounted checks discover the parent framework checkout.
- Source-relative links would break in an independent repository. Matching public sources now use immutable public revision URLs. Unpublished or changed sources are shown as checkout paths, with source hashes recorded in the inventory and source context described at the repository entry.
- A maintainer sentence overstated the rewritten Python checker's handling of external source line anchors. It now states that generated location/signature drift is covered by the API generator's --check mode.
- Framework contract review found no contradiction in Controller Event capabilities, owner-scoped Services, Clock driving, UI attributes, plugin/resource ownership, Config/Save/Diagnostics or utility guidance.

The reference prerequisite returned an empty graph from the framework root. This is recorded as missing dependency metadata, not interpreted as proof of no dependencies. No production graph entries or implementation changes were invented for publication.

## Observed validation

The independent forward check followed the staged skill into project guides, conventions, UI attributes and the CLI dependency workflow. It executed bundle-only validation, validation with the framework source checkout, and API --check across eight production projects successfully.

The package-based managed example was restored from a fresh local ObFramework package (0.0.0-docs-publish) and printed `Documentation examples passed.` This temporary package version is validation output, not a published NuGet release.

Negative probes confirmed rejection of missing links, checkout-escaping relative links, public test-project dependencies and changed source hashes. Probes were removed. Canonical and forwarding skill frontmatter checks passed, and the intended publication content had no credential-pattern matches.

Public user guides contain no test-project citations. Implementation tests remain with the source checkout. Native Unity/Godot/UnrealSharp acceptance and a full NUnit run were not performed for this documentation extraction.

The source snapshot includes implementation changes not yet published to the source repository. Publishing the documentation child repository does not publish those changes; source integration is limited to submodule/entry/documentation paths.
