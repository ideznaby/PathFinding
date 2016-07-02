using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace AIproject
{
    public partial class Form1 : Form
    {
        public problem P;
        public Form1()
        {
            InitializeComponent();
        }

        private void Online_Click(object sender, EventArgs e)
        {
            LRTA A = new LRTA(P);
            state CS = P.firststate;
            draw_world(P.goal,P.firststate);
            int i = 0;
            float distance = 0;
            while(true){
                int CA = A.LRTA_Agent(CS);
                if (CA == 8)
                    break;
                actions acts = new actions(CS);
                state next = acts[CA];
                distance += problem.dist(CS, next) / problem.BlockSize;
                Point t = new Point(next.X + 2, next.Y+2);
                CS = next;
                Thread.Sleep(200);
                Agent.Location = t;
                Agent.Update();
                i++;
            }
            MessageBox.Show("moves : " + i + "\n distance traveled : " + distance);
        }
        public void draw_world(state G,state S)
        {
            Pen pen = new Pen(Color.Black, 2);
            Pen pen1 = new Pen(Color.Blue, 1);
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            Agent.Location = new Point(S.X + 2, S.Y + 2);
            Agent.Update();
            foreach (obstacle ob in problem.obs)
            {
                g.FillRectangle(Brushes.Black, ob.ob.X, ob.ob.Y, problem.BlockSize, problem.BlockSize);
            }
            for (int i = 0; i <= problem.width + problem.BlockSize; i += problem.BlockSize)
                g.DrawLine(pen1, i, 0, i, problem.height + problem.BlockSize);
            for (int i = 0; i <= problem.height+problem.BlockSize; i += problem.BlockSize)
                g.DrawLine(pen1, 0, i, problem.width + problem.BlockSize, i);
            pen = new Pen(Color.Red, 2);
            g.DrawEllipse(pen, G.X, G.Y, problem.BlockSize, problem.BlockSize);
        }

        private void Astar_Click(object sender, EventArgs e)
        {
            AStar AS = new AStar(P);
            draw_world(P.goal,P.firststate);
            state[] solution = AS.AS();
            float distance = 0;
            if (solution.Length > 1)
            {
                MessageBox.Show("i found the answer now i will show it to u");
                state previous = P.firststate;
                foreach (state S in solution)
                {
                    distance += problem.dist(previous, S) / problem.BlockSize;
                    if (S.lastaction >= 8)
                    {
                        MessageBox.Show("failure");
                        break;
                    }
                    Point t = new Point(S.X + 2 , S.Y + 2);
                    Agent.Location = t;
                    Agent.Update();
                    Thread.Sleep(200);
                    previous = S;
                }
                MessageBox.Show("moves : " + solution.Length + "\n distance traveled : " + distance);
            }
            else
                if (solution[0].lastaction == 8)
                    MessageBox.Show("failure");
                else
                    if (solution[0].lastaction == 9)
                        MessageBox.Show("out of memory");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Point p = new Point(0, 0);
            Agent.Location = p;
        }

        private void RBFS_Click(object sender, EventArgs e)
        {
            AStar AS = new AStar(P);
            draw_world(P.goal,P.firststate);
            state[] solution = AS.RecursiveBestFirstSearch();
            float distance = 0;
            if (solution.Length > 1)
            {
                MessageBox.Show("i found the answer now i will show it to u");
                state previous = P.firststate;
                foreach (state S in solution)
                {
                    distance += problem.dist(previous, S) / problem.BlockSize;
                    if (S.lastaction >= 8)
                    {
                        MessageBox.Show("failure");
                        break;
                    }
                    Point t = new Point(S.X + 2, S.Y + 2);
                    Agent.Location = t;
                    Agent.Update();
                    Thread.Sleep(200);
                    previous = S;
                }
                MessageBox.Show("moves : " + solution.Length + "\n distance traveled : " + distance);
            }
            else
                if (solution[0].lastaction == 8)
                    MessageBox.Show("failure");
                else
                    if (solution[0].lastaction == 9)
                        MessageBox.Show("out of memory");

        }

        private void SMA_Click(object sender, EventArgs e)
        {
            AStar AS = new AStar(P);
            draw_world(P.goal,P.firststate);
            Agent.Update();
            state[] solution = AS.SMAStar();
            float distance = 0;
            if (solution.Length > 0)
            {
                MessageBox.Show("i found the answer now i will show it to u");
                state previous = P.firststate;
                foreach (state S in solution)
                {
                    distance += problem.dist(previous, S)/problem.BlockSize;
                    if (S.lastaction >= 8)
                    {
                        MessageBox.Show("failure");
                        break;
                    }
                    Point t = new Point(S.X + 2, S.Y + 2);
                    Agent.Location = t;
                    Agent.Update();
                    Thread.Sleep(200);
                    previous = S;
                }
                MessageBox.Show("moves : " + solution.Length + "\n distance traveled : " + distance);
            }
            else
                if (solution[0].lastaction == 8)
                    MessageBox.Show("failure");
                else
                    if (solution[0].lastaction == 9)
                        MessageBox.Show("out of memory");
        }

        private void Load_Click(object sender, EventArgs e)
        {
            problem.BlockSize = 50;
            OpenFileDialog OFD = new OpenFileDialog();

            DialogResult result = OFD.ShowDialog(); // Show the dialog.
            OFD.Title = "Please select input file";
            if (result == DialogResult.OK) // Test result.
            {
                string file = OFD.FileName;
                try
                {
                    problem.sr = new StreamReader(file);
                    P = new problem();
                    Agent.Size = new Size(problem.BlockSize - 2, problem.BlockSize - 2);
                    draw_world(P.goal, P.firststate);
                    this.Online.Enabled = true;
                    this.Astar.Enabled = true;
                    this.RBFS.Enabled = true;
                    this.SMA.Enabled = true;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("there was a problem opening the file this is error code : " + ex);
                }
            }
        }



    }
    public class obstacle
    {
        public Point ob;
        public bool pointinobs(state P)
        {
            if (P.X == ob.X && P.Y == ob.Y)
                return true;
            return false;
        }
        public obstacle(Point p)
        {
            ob = p;
        }
        /*public Point[] polygon;
        public obstacle(Point[] ob)
        {
            polygon = ob;
        }
        public bool pointInPolygon(state P)
        {
            int x = P.X;
            int y = P.Y;
            int i, j = polygon.Length - 1;
            bool oddNodes = false;
            for (i = 0; i < polygon.Length; i++)
            {
                if ((polygon[i].Y < y && polygon[j].Y >= y
                || polygon[j].Y < y && polygon[i].Y >= y)
                && (polygon[i].X <= x || polygon[j].X <= x))
                {
                    oddNodes ^= ((float) polygon[i].X + (float) (y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < x);
                }
                j = i;
            }
            return oddNodes;
        }*/
    }
    public class state
    {
        public float f;
        public float g = 0;
        public int d = 0;
        private Point P;
        public state parent;
        public int lastaction;
        public void setparent(state Parent)
        {
            parent = Parent;
            g = Parent.g + problem.dist(Parent, this)/problem.BlockSize;
            d = Parent.d + 1;
        }
        public state(int x, int y)
        {
            P.X = x;
            P.Y = y;
        }
        public int X
        {
            get
            {
                return P.X;
            }
        }
        public int Y
        {
            get
            {
                return P.Y;
            }
        }
    }
    public class actions
    {
        state CS;
        public actions(state cs){
            CS = cs;
        }
        state next;
        public state this[int action]
        {
            get
            {
                switch (action)
                {
                    case 0:
                        next = (CS.X != 0) ? new state(CS.X - problem.BlockSize, CS.Y) : CS;
                        break;
                    case 1:
                        next = (CS.X != 0 && CS.Y != 0) ? new state(CS.X - problem.BlockSize, CS.Y - problem.BlockSize) : CS;
                        break;
                    case 2:
                        next = (CS.Y != 0) ? new state(CS.X, CS.Y - problem.BlockSize) : CS;
                        break;
                    case 3:
                        next = (CS.X != problem.width && CS.Y != 0) ? new state(CS.X + problem.BlockSize, CS.Y - problem.BlockSize) : CS;
                        break;
                    case 4:
                        next = (CS.X != problem.width) ? new state(CS.X + problem.BlockSize, CS.Y) : CS;
                        break;
                    case 5:
                        next = (CS.X != problem.width && CS.Y != problem.height) ? new state(CS.X + problem.BlockSize, CS.Y + problem.BlockSize) : CS;
                        break;
                    case 6:
                        next = (CS.Y != problem.height) ? new state(CS.X, CS.Y + problem.BlockSize) : CS;
                        break;
                    case 7:
                        next = (CS.X != 0 && CS.Y != problem.height) ? new state(CS.X - problem.BlockSize, CS.Y + problem.BlockSize) : CS;
                        break;
                }
                bool b = true;
                foreach (obstacle o in problem.obs)
                    if (o.pointinobs(next))
                        b = false;
                return b?next:CS;
            }
        }
    }
    public class problem
    {
        public state firststate;
        public state goal;
        public static int width;
        public static int height;
        public static int BlockSize = 50;
        public static obstacle[] obs;
        public static StreamReader sr;
        public problem()
        {
            string[] world = sr.ReadLine().Split(',');
            if (Convert.ToInt32(world[1]) > 1550 / BlockSize)
                BlockSize = 1550 / Convert.ToInt32(world[1]);
            if (Convert.ToInt32(world[0]) > 650 / BlockSize)
                BlockSize = 650 / Convert.ToInt32(world[0]);
            width = (Convert.ToInt32(world[1]) -1 )*BlockSize;
            height = (Convert.ToInt32(world[0]) - 1)*BlockSize;
            string[] Start = sr.ReadLine().Split(',');
            firststate = new state((Convert.ToInt32(Start[0])-1) * BlockSize, (Convert.ToInt32(Start[1])-1) * BlockSize);
            if (firststate.X > width || firststate.Y > height || firststate.X < 0 || firststate.Y <0)
            {
                MessageBox.Show("Start Point is out of the range of the world");
            }
            string[] End = sr.ReadLine().Split(',');
            goal = new state((Convert.ToInt32(End[0])-1) * BlockSize, (Convert.ToInt32(End[1])-1) * BlockSize);
            if (goal.X > width || goal.Y > height || goal.X < 0 || goal.Y < 0)
                MessageBox.Show("Goal Point is out of the range of the world");
            int n = Convert.ToInt32(sr.ReadLine());
            string[] obstacles = sr.ReadLine().Split('-');
            obs = new obstacle[n];
            for (int i = 0; i < n; i++)
            {
                string[] obstacle = obstacles[i].Split(',');
                Point obstract = new Point((Convert.ToInt32(obstacle[0])-1) * BlockSize, (Convert.ToInt32(obstacle[1])-1) * BlockSize);
                obs[i] = new obstacle(obstract);
            }
            sr.Close();
        }
        public static float dist(state s, state s1)
        {
            return (float)Math.Sqrt(Math.Pow(s.Y - s1.Y, 2) + Math.Pow(s.X - s1.X, 2));
        }
        public state[] Succesorfunction(state currentstate)
        {
            state[] s = new state[8];
            actions a = new actions(currentstate);
            for (int i = 0; i < 8; i++)
            {
                s[i] = a[i];
                if (s[i] == currentstate)
                {
                    s[i] = new state(0, 0);
                    s[i].lastaction = -1;
                    continue;
                }
                s[i].lastaction = i;
            }
            return s;
        }
        public bool GoalTest(state cs)
        {
            return (cs.X == goal.X && cs.Y == goal.Y);
        }
    }
    public class LRTA
    {
        problem P;
        float[,] H = new float[problem.width+1,problem.height+1];
        state s;
        int a;
        state[, ,] result = new state[8,problem.width+1,problem.height+1];
        public LRTA(problem p)
        {
            P = p;
        }
        private float h(state s)
        {
            return problem.dist(s, P.goal);
        }
        private float LRTA_Cost(state s, state s1,state n)
        {
            if (s1 == null)
                return h(n);
            else
                return problem.dist(s, s1) + H[s1.X, s1.Y] + 500;//to penaltalize repeating moves
        }
        public int LRTA_Agent(state s1)
        {
            if(P.GoalTest(s1))
                return 8;
            if (H[s1.X, s1.Y] == 0)
                H[s1.X, s1.Y] = h(s1);
            if (s != null)
            {
                result[a, s.X, s.Y] = s1;
                float min = 99999999999;
                for(int i = 0;i<=7;i++)
                {
                    actions Acts = new actions(s);
                    if (Acts[i] == s)
                        continue;
                    if(LRTA_Cost(s,result[i,s.X,s.Y],Acts[i]) < min)
                        min = LRTA_Cost(s, result[i, s.X, s.Y], Acts[i]);
                }
                H[s.X, s.Y] = min;
            }
            float min1 = 99999999999;
            int b = 0;
            for (int i = 0; i <= 7; i++)
            {
                actions Acts = new actions(s1);
                if (Acts[i] == s1)
                    continue;
                if (LRTA_Cost(s1, result[i, s1.X, s1.Y], Acts[i]) < min1)
                {
                    min1 = LRTA_Cost(s1, result[i, s1.X, s1.Y], Acts[i]);
                    b = i;
                }
            }
            a = b;
            s = s1;
            return a;
        }
    }
    class Heap
    {
        private state[] heapArray;
        private int maxSize;
        private int currentSize;
        public Heap(int maxHeapSize)
        {
            maxSize = maxHeapSize;
            currentSize = 0;
            heapArray = new state[maxSize];
        }
        public bool IsEmpty()
        { return currentSize == 0; }
        public bool Insert(state S)
        {
            if (currentSize == maxSize)
                return false;
            heapArray[currentSize] = S;
            CascadeUp(currentSize++);
            return true;
        }
        public void CascadeUp(int index)
        {
            int parent = (index - 1) / 2;
            state bottom = heapArray[index];
            while (index > 0 && heapArray[parent].f > bottom.f)
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }
        public state Remove() // Remove maximum value node
        {
            state root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            CascadeDown(0);
            return root;
        }
        public state RemoveLast()
        {
            state last = heapArray[heapArray.Length-1];
            currentSize--;
            return last;
        }
        public void CascadeDown(int index)
        {
            int largerChild;
            state top = heapArray[index];
            while (index < currentSize / 2)
            {
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                if (rightChild < currentSize && heapArray[leftChild].f > heapArray[rightChild].f)
                    largerChild = rightChild;
                else
                    largerChild = leftChild;
                if (top.f <= heapArray[largerChild].f)
                    break;
                heapArray[index] = heapArray[largerChild];
                index = largerChild;
            }
            heapArray[index] = top;
        }
    }
    public class AStar
    {
        problem P;
        public AStar(problem currentP)
        {
            P = currentP;
        }
        public float h(state current)
        {
            return problem.dist(current, P.goal)/problem.BlockSize;
        }
        public state[] AS()
        {
            Heap fring = new Heap(10000000);
            P.firststate.f = h(P.firststate);
            fring.Insert(P.firststate);
            while (true)
            {
                if (fring.IsEmpty())
                {
                    state[] SOLUTION = new state[1];
                    SOLUTION[0].lastaction = 8;
                    return SOLUTION;
                }
                state a = fring.Remove();
                if (P.GoalTest(a))
                {
                    state[] SOLUTION = new state[a.d];
                    int i = 0;
                    state s = a;
                    while (s.parent != null)
                    {
                        SOLUTION[i] = s;
                        i++;
                        s = s.parent;
                    }
                    Array.Reverse(SOLUTION);
                    return SOLUTION;
                }
                else
                    foreach (state s in P.Succesorfunction(a))
                    {
                        if (s.lastaction == -1)
                            continue;
                        s.setparent(a);
                        s.f =(float) h(s) + s.g;
                        if (!fring.Insert(s))
                        {
                            state[] SOLUTION = new state[1];
                            SOLUTION[0].lastaction = 9;
                            return SOLUTION;
                        }
                    }
            }
        }
        public state[] RecursiveBestFirstSearch()
        {
            float f;
            return RBFS(P.firststate, 999999999999,out f);
        }
        public state[] RBFS(state a , float f_limit,out float f_cost)
        {
            if (P.GoalTest(a))
            {
                state[] SOLUTION = new state[a.d];
                int i = 0;
                state s = a;
                while (s.parent != null)
                {
                    SOLUTION[i] = s;
                    i++;
                    s = s.parent;
                }
                Array.Reverse(SOLUTION);
                f_cost = 0;
                return SOLUTION;
            }
            state[] succesor = P.Succesorfunction(a);
            int count = 0;
            foreach (state s in succesor)
            {
                if (s.lastaction == -1)
                    continue;
                count++;
                s.setparent(a);
                s.f = (float)h(s) + s.g;
                s.f = Math.Max(s.f, a.f);
            }
            if (count == 0)
            {
                state[] SOLUTION = new state[1];
                SOLUTION[0] = new state(0, 0);
                SOLUTION[0].lastaction = 8;
                f_cost = 999999999999;
                return SOLUTION;
            }
            while (true)
            {
                float best = 999999999;
                float alternative = 999999999;
                state beststate= new state(0,0);
                foreach (state s in succesor)
                {
                    if (s.lastaction == -1)
                        continue;
                    if (s.f < best)
                    {
                        alternative = best;
                        best = s.f;
                        beststate = s;
                    }
                    else if (s.f < alternative)
                        alternative = s.f;
                }
                if (best > f_limit)
                {
                    state[] SOLUTION = new state[1];
                    SOLUTION[0] = new state(0, 0);
                    SOLUTION[0].lastaction = 8;
                    f_cost = best;
                    return SOLUTION;
                }
                state[] result = RBFS(beststate, Math.Min(f_limit, alternative), out beststate.f);
                if (result[0].lastaction != 8) { f_cost = 0; return result; }
            }
        }
        public state[] SMAStar()
        {
                int max_depth = 40;
                Heap fring = new Heap(10000);
                P.firststate.f = h(P.firststate);
                fring.Insert(P.firststate);
                while (true)
                {
                    if (fring.IsEmpty())
                    {
                        state[] SOLUTION = new state[1];
                        SOLUTION[0].lastaction = 8;
                        return SOLUTION;
                    }
                    state a = fring.Remove();
                    if (P.GoalTest(a))
                    {
                        state[] SOLUTION = new state[a.d];
                        int i = 0;
                        state s = a;
                        while (s.parent != null)
                        {
                            SOLUTION[i] = s;
                            i++;
                            s = s.parent;
                        }
                        Array.Reverse(SOLUTION);
                        return SOLUTION;
                    }
                    else
                        foreach (state s in P.Succesorfunction(a))
                        {
                            if (!P.GoalTest(s) && s.d == max_depth)
                                s.f = 99999999999;
                            if (s.lastaction == -1)
                                continue;
                            s.setparent(a);
                            s.f = (float)h(s) + s.g;
                            if (!fring.Insert(s))
                            {
                                state badnode = fring.RemoveLast();
                                badnode.lastaction = -1;
                                //MessageBox.Show("freeing memory");
                            }
                        }
                }
        }
    }

}
