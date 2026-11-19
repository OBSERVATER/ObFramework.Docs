# ObFramework glossary

## Business data

**Blackboard**: A shared space in which business code writes current data and readers obtain the parts they need. Reading expresses the data already written; it does not require a writer to refresh data in response.
_Avoid_: request-driven refresh cache.

**Query**: An operation that reads, filters or combines current business data without refreshing or mutating its sources. Its result may be a value, object or collection; it does not promise an automatically created snapshot.

## Releases

**ObFramework release**
A set of runtime, CLI and Unity adapter packages built from the same source revision and published together under a single version.

**Release request**
A maintainer's declaration of the exact version to publish from a specific source revision.
