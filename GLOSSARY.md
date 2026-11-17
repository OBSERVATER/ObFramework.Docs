# Business data glossary

**Blackboard**: A shared space in which business code writes current data and readers obtain the parts they need. Reading expresses the data already written; it does not require a writer to refresh data in response.
_Avoid_: request-driven refresh cache.

**Query**: An operation that reads, filters or combines current business data without refreshing or mutating its sources. Its result may be a value, object or collection; it does not promise an automatically created snapshot.
