# PathFinding
Pathfinding or pathing is the plotting, by a computer application, of the shortest route between two points. Also usually there are obstacles between these two points which robot should avoid.

Here is a program to solve pathfinding problem with 4 algorithms(A* , RBFS , LRTA* , SMA*) that we will describe them in this article later but first i want to talk about using the program.

The code is written in C# language and in visual studio 2010

1 . How to use program :

First of all you must create an input file. There are 5 of them along with program source that you can use.

Input files of program are in this format :

First Line - height and width of the world separated with comma (ex : 6,20 means the height of the world is 6 block and it's width is 20 block)

Second Line -  X and Y position of the robot in the screen separated with comma (ex : 1,2 means the robot is in the first column and second row)

Third Line - X and Y position of the goal separated by comma (ex : 14,5 means the goal which robot will go is in the 14th column and 5th row)

Fourth Line - Number of obstacles in the way (ex : 3 means there are 21 obstacle in the way)

Fifth Line - X and Y of each obstacles separated by comma and obstacles separated by line between them (ex : 2,2-2,3-2,4 means there is an obstacle in 2nd column and 2nd row and also there is another obstacle in 2,3 and another in 2,4)

Then you can run the program and press "Load Input File" button to open Input file you created ( or the ready ones from program source ) then you have to Click on one of the algorithm buttons to solve the program using that algorithm. If you click on online button the robot simply has no knowledge of the map so it's gonna begin discovering and learning immediately or if you click any of others the robot is gonna think a while( depending on hardness of your problem ) and find the solution and then will move to goal point and in the end will show the steps and distance traveled.

ScreenShot :
![ScreenShot](/AIproject/screenshots/input4.JPG)

2 . About Source Code

Source code of program is very easy to understand. The offline algorithms are in Astar class and online in a separated class named LRTA and also there is problem class which identifies problem and reads Input files and transfers them to variables which algorithms can easily read and solve, Obstacle class which identifies the obstacles, Actions which identifies the actions robot can take and their costs ( distances which robot goes with each action), State which identifies separate states of program and also there is a Heap class to use for fring in Astar algorithm.

3 . LRTA*

LRTA* ( Learning Real Time A* ) is an online algorithm ( means that robot has no knowledge about it's environment and only knows the location of it's goal so it has to move around and discover it's surroundings). online algorithms are all about sense move sense move sense ... ( opposite of offline algorithms which are about sense and move ) so in every iteration the robot first senses the environment and updates It's knowledge ( in LRTA this is a table named H ) and then moves then again senses and updates and then moves again and so on ...

Here is pseudocode for LRTA* :
```
function LRTA*-COST (s, a, s’,H) return a cost estimate
        if s’ is undefined return h(s)
        else return c(s, a, s’) + H[s’]
function LRTA*-AGENT (s’) return an action
        input: s’, a percept identifying current state
        static: result, a table indexed by action and state, initially empty
                 H, a table of cost estimates indexed by state, initially empty
                 s,a, the previous state and action, initially null


if GOAL-TEST (s’) then return stop
if s’ is a new state (not in H) then H[s’] ← h(s’)
unless s is null
        result[a,s] ← s’
        H[s] ← MIN LRTA*-COST (s, b, result [b, s], H)
                   b ∈ ACTIONS (s)
a ← an action b in ACTIONS (s’) that minimizes LRTA*-COST (s’, b, result [b, s’], H)
s ← s’
return a
```

4. A*

A* is an offline algorithm (see the description above in LRTA to know the difference between offline and online algorithm) and is a member of Best-first algorithms. It uses f-cost (which is sum of distance we traveled to this point and estimated distance to goal) to find the next best state to choose. To read more about A* you can take a look at my previous article 8-puzzle solver using A*. A* is very useful and very fast algorithm but it has storage problems.

5 . RBFS

RBFS (Recursive Best First Search) is an offline algorithm (see the description above in LRTA to know the difference between offline and online algorithm) and actually its a upgraded version of A* to solve the storage problem. It's a recursive algorithm and only stores one node in memory so because it uses too little memory Its very slow.

Pseudocode for RBFS :
```
function RECURSIVE-BEST-FIRST-SEARCH(problem) return a solution or failure
         return RFBS(problem,MAKE-NODE(INITIAL-STATE[problem]),∞)
function RFBS( problem, node, f_limit) return a solution or failure and a new f-cost limit
         if GOAL-TEST[problem](STATE[node]) then return node
         successors ← EXPAND(node, problem)
         if successors is empty then return failure, ∞
         for each s in successors do
              f [s] ← max(g(s) + h(s), f [node])
         repeat
              best ← the lowest f-value node in successors
              if f [best] > f_limit then return failure, f [best]
              alternative ← the second lowest f-value among successors
              result, f [best] ← RBFS(problem, best, min(f_limit, alternative))
              if result ≠ failure then return result
```

6 . SMA*

SMA* (Simplified Memory Bounded A*) is another algorithm based on A* and it works just like it. The only advantage of SMA* is that it uses bounded memory. SMA* is a very efficient and useful algorithm in pathfinding.

Pseudocode for SMA* (taken from http://en.wikipedia.org/wiki/SMA*) : 
```
function SMA-star(problem): path
  queue: set of nodes, ordered by f-cost;
begin
  queue.insert(problem.root-node);
 
  while True do begin
    if queue.empty() then return failure; //there is no solution that fits in the given memory
    node := queue.begin(); // min-f-cost-node
    if problem.is-goal(node) then return success;
 
    s := next-successor(node)
    if !problem.is-goal(s) && depth(s) == max_depth then
        f(s) := inf; 
        // there is no memory left to go past s, so the entire path is useless
    else
        f(s) := max(f(node), g(s) + h(s));
        // f-value of the successor is the maximum of
        //      f-value of the parent and 
        //      heuristic of the successor + path length to the successor
    endif
    if no more successors then
       update node-s f-cost and those of its ancestors if needed
 
    if node.successors ⊆ queue then queue.remove(node); 
    // all children have already been added to the queue via a shorter way
    if memory is full then begin
      badNode := shallowest node with highest f-cost;
      for parent in badNode.parents do begin
        parent.successors.remove(badNode);
        if needed then queue.insert(parent); 
      endfor
    endif
 
    queue.insert(s);
  endwhile
end
```
