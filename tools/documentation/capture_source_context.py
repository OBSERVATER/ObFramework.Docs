"""Record which current product sources match an explicitly selected public Git revision."""
import argparse
import hashlib
import json
from pathlib import Path
import subprocess

parser = argparse.ArgumentParser(description=__doc__)
parser.add_argument('--source-root', type=Path, required=True)
parser.add_argument('--published-ref', required=True, help='A known published commit or remote-tracking ref, such as origin/master')
args = parser.parse_args()
source = args.source_root.resolve()
bundle = Path(__file__).resolve().parents[2]
context_path = bundle / 'source-context.json'
previous = json.loads(context_path.read_text(encoding='utf-8'))

def git(*params):
    return subprocess.check_output(['git', '-C', str(source), '-c', 'safe.directory=' + source.as_posix(), *params], stderr=subprocess.PIPE)

published = git('rev-parse', args.published_ref + '^{commit}').decode().strip()
head = git('rev-parse', 'HEAD').decode().strip()
tracked = set(git('ls-tree', '-r', '--name-only', '-z', published).decode().split('\0')) - {''}
changed = set(git('diff', '--name-only', '-z', published, '--').decode().split('\0')) - {''}
projects = ['Framework', 'ObFramework.Adapter.Unity', 'ObFramework.Adapter.Godot',
            'ObFramework.Adapter.UnrealSharp', 'ObFramework.Cli', 'ObFramework.Editor.Core',
            'ObFramework.Generator', 'ObFramework.Generator.Unity']
files = {path.relative_to(source).as_posix() for project in projects for path in (source / project).rglob('*.cs')
         if not {'.git', 'artifacts', 'obj', 'bin'} & set(path.relative_to(source / project).parts)}
hashes = {path: hashlib.sha256((source / path).read_bytes()).hexdigest() for path in sorted(files & (tracked - changed))}
context = {'source_repository': previous['source_repository'], 'published_commit': published,
           'local_source_commit': head, 'snapshot': 'working-tree', 'published_hashes': hashes, 'note': previous['note']}
context_path.write_text(json.dumps(context, indent=2) + '\n', encoding='utf-8')
print(f'Recorded {len(hashes)} matching public product-source files. Git metadata is local; verify that --published-ref is actually available to readers before publishing.')
