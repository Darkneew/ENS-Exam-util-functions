using System.Collections.Generic;

namespace TPinfo
{
    class NonOrientedGraph
    {
        private int _size;
        private int _def;
        private int[,] _edges;
        private bool[,] _connection;

        public int Def { get => _def; }

        public int this[int a, int b] { get { return _edges[a, b]; } }

        public bool IsConnected(int a, int b) { return _connection[a, b]; }

        public void AddEdge(int a, int b, int val)
        {
            _connection[a, b] = true;
            _edges[a, b] = val;
            _connection[b, a] = true;
            _edges[b, a] = val;
        }

        public void RemoveEdge(int a, int b)
        {
            _connection[a, b] = false;
            _edges[a, b] = _def;
            _connection[b, a] = false;
            _edges[b, a] = _def;
        }

        public void InitialiseSelfEdge()
        {
            for (int i = 0; i < _size; i++)
            {
                _connection[i, i] = true;
                _edges[i, i] = 0;
            }
        }

        public NonOrientedGraph(int size, int def)
        {
            _size = size;
            _def = def;
            _edges = new int[size, size];
            _connection = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    _edges[i, j] = def;
                    _connection[i, j] = false;
                }
            }
        }

        public List<int> PreDFSOrder(int u)
        {
            List<int> order = new List<int>(_size);
            bool[] visited = new bool[_size];
            for (int i = 0; i < _size; i++) visited[i] = false;
            Stack<int> stack = new Stack<int>(_size*_size);
            stack.Push(u);
            while (stack.Count > 0)
            {
                int s = stack.Pop();
                if (visited[s]) continue;
                visited[s] = true;
                order.Add(s);
                for (int i = 0;  i < _size; i++)
                {
                    if (visited[i]) continue;
                    if (_connection[s, i]) stack.Push(i);
                }
            }
            return order;
        }

        public List<int> PostDFSOrder(int u)
        {

            List<int> order = new List<int>(_size);
            bool[] visited = new bool[_size];
            bool[] added = new bool[_size];
            for (int i = 0; i < _size; i++) { visited[i] = false; added[i] = false; }
            Stack<int> stack = new Stack<int>(_size * _size);
            stack.Push(u);
            while (stack.Count > 0)
            {
                int s = stack.Pop();
                if (added[s]) continue;
                if (visited[s])
                {
                    added[s] = true;
                    order.Add(s);
                    continue;
                }
                visited[s] = true;
                stack.Push(s);
                for (int i = 0; i < _size; i++)
                {
                    if (visited[i]) continue;
                    if (_connection[s, i]) stack.Push(i);
                }
            }
            return order;
        }

        public List<int> BFSOrder(int u)
        {
            List<int> order = new List<int>(_size);
            bool[] added = new bool[_size];
            for (int i = 0; i < _size; i++) added[i] = false;
            Queue<int> queue = new Queue<int>(_size);
            queue.Enqueue(u); added[u] = true;
            while (queue.Count > 0)
            {
                int s = queue.Dequeue();
                order.Add(s);
                for (int i = 0; i < _size; i++)
                {
                    if (added[i]) continue;
                    if (!_connection[s, i]) continue;
                    queue.Enqueue(i);
                    added[i] = true;
                }
            }
            return order;
        }

        public NonOrientedGraph GetConnexGraph(int s)
        {
            List<int> vertices = BFSOrder(s);
            NonOrientedGraph graph = new NonOrientedGraph(vertices.Count, _def);
            for (int i = 0; i < vertices.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (_connection[vertices[i], vertices[j]])
                    {
                        graph.AddEdge(i, j, _edges[vertices[i], vertices[j]]);
                    }
                }
            }
            return graph;
        }
    }
}
