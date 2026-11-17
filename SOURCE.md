# Source context

The source repository is [OBSERVATER/ObFramework](https://github.com/OBSERVATER/ObFramework). Source paths in these guides are relative to that checkout, while documentation, skill and example paths are relative to ObFramework.Docs.

This snapshot describes the current source working tree, including local changes that are not yet published. It is not a statement that those APIs are available in a released NuGet version or in the source repository's public default branch.

`source-context.json` records the local source base and a known public source commit. Links to public source are pinned to that public commit only when the corresponding source bytes match. A location shown as a code path, optionally followed by a line number, requires the matching source checkout. The API inventory records hashes of the source it describes.

Run `python tools/documentation/check_docs.py --source-root <framework-checkout>` to check the local source against the inventory. A standalone check without source validates the documentation bundle only and reports that source verification was skipped. The checker does not make live network requests to validate external links.

Source build commands in adapter/CLI guides run in the framework checkout at the stated directory. Documentation tooling and package-example commands run in the ObFramework.Docs root, which is `ai/` in the integrated layout.
