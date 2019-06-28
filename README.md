# JobProcessor

## The Challenge

Imagine we have a list of jobs, each represented by a character. Because certain jobs must be done before others, a job may have a dependency on another job. For example, `a` may depend on `b`, meaning the final sequence of jobs should place `b` before `a`. If `a` has no dependency, the position of `a` in the final sequence does not matter.

- Given you’re passed an empty string (no jobs), the result should be an empty sequence.

- Given the following job structure:
`a =>`
The result should be a sequence consisting of a single job a.

- Given the following job structure:
  `a =>  | b =>  | c =>`
The result should be a sequence containing all three jobs abc in no significant order.

- Given the following job structure:
  `a => | b => c | c =>`
The result should be a sequence that positions c before b, containing all three jobs abc.

- Given the following job structure:
  `a => | b => c | c => f | d => a | e => b | f =>`
The result should be a sequence that positions f before c, c before b, b before e and a before d containing all six jobs abcdef.

- Given the following job structure:
  `a => | b => | c => c`
The result should be an error stating that `jobs can’t depend on themselves`.

- Given the following job structure:
`a => | b => c | c => f | d => a | e => | f => b`
The result should be an error stating that `jobs can’t have circular dependencies`.


## Sample input and output

input : `a => | b => c | c =>`   
output : `acb` or `cba`

input : `a => | b => b | c =>`   
output : `jobs can’t depend on themselves`

input : `a => | b => c | c => b`   
output : `jobs can’t have circular dependencies`


## Solving approach

#### JobProcessLib
Library responsible for processing the jobs.
- `JobManager` is the class will be used by the client. Client will call the method `public string GetSortedJobs(string input)` which will return the properly sequeced jobs or throw error if any exception occurs. There are some private methods which are self explanatory enough.
- `SingleJobSequence` class is used for intermediate data structure after parsing the input to have a single jobs' data in a good format.
- `Job`class is used for having the job in proper format with all its' dependency and used over libraries.
- `JobSequencer` class has been used as a wrapper class over the main sorting library, so that if need to change the sort library later we can do that with minimal changes. Also the out format logic has been implemented here.

#### SortLib
Here Topological sort has been used to sort jobs with dependencies. The pseudocode of the algorithm is given below -

```
foreach job in jobs
  visitjob(job)
  
function visitjob(job j)
  if j is already visited
    and if j is in process then there is circular dependency
  
  if j is not visited
    mark j as in process
    get dependency of j
    foreach dependency of j
      visitjob(dependency)
    
    mark j as processed
    add j to the sorted list
```

#### UnitTests
There are some test cases in `JobManagerTests` class.
