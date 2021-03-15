// C# program for the above approach
using System;
using System.Collections.Generic;

public class GFG
{
    static int N = 1000;
    static List<List<int>> adj = new List<List<int>>();
    List<int> AL = new List<int>();
    // Function to print the complete DFS-traversal
    static void dfsUtil(int u, int node, bool[] visited,
                        List<List<int>> road_used,
                        int parent, int it)
    {
        int c = 0;

        // Check if all th node is visited or not
        // and count unvisited nodes
        for (int i = 0; i < node; i++)
            if (visited[i])
                c++;

        // If all the node is visited return;
        if (c == node)
            return;

        // Mark not visited node as visited
        visited[u] = true;

        // Track the current edge
        road_used.Add(new List<int>() { parent, u });

        // Print the node
        //Console.Write(u + " ");
        AL.Add(u);

        // Check for not visited node and proceed with it.
        foreach (int x in adj[u])
        {
            // call the DFs function if not visited
            if (!visited[x])
            {
                dfsUtil(x, node, visited, road_used, u,
                        it + 1);
            }
        }
        // Backtrack through the last
        // visited nodes
        for (int y = 0; y < road_used.Count; y++)
        {
            if (road_used[y][1] == u)
            {
                dfsUtil(road_used[y][0], node, visited,
                        road_used, u, it + 1);
            }
        }
    }

    // Function to call the DFS function
    // which prints the DFS-travesal stepwise
    static void dfs(int node)
    {
        // Create a array of visited ndoe
        bool[] visited = new bool[node];

        // Vector to track last visited road
        List<List<int>> road_used = new List<List<int>>();

        // Initialize all the node with false
        for (int i = 0; i < node; i++)
        {
            visited[i] = false;
        }

        // call the function
        dfsUtil(0, node, visited, road_used, -1, 0);
    }

    // Function to insert edges in Graph
    static void insertEdge(int u, int v)
    {
        adj[u].Add(v);
        adj[v].Add(u);
    }
    static public void Main()
    {
        // number of nodes and edges in the graph
        int node = 11;
        for (int i = 0; i < N; i++)
        {
            adj.Add(new List<int>());
        }

        // Function call to create the graph
        insertEdge(0, 1);
        insertEdge(0, 2);
        insertEdge(1, 5);
        insertEdge(1, 6);
        insertEdge(2, 4);
        insertEdge(2, 9);
        insertEdge(6, 7);
        insertEdge(6, 8);
        insertEdge(7, 8);
        insertEdge(2, 3);
        insertEdge(3, 9);
        insertEdge(3, 10);
        insertEdge(9, 10);

        // Call the function to print
        dfs(node);
    }
}
// This code is contributed by rag2127