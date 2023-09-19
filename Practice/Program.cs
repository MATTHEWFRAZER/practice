using System;
using System.Diagnostics;

// See https://aka.ms/new-console-template for more information


var node1 = new Methods.Node<int>(7, new List<Methods.Node<int>> { new Methods.Node<int>(1, null) , new Methods.Node<int>(2, null) });
var node2 = new Methods.Node<int>(4, new List<Methods.Node<int>> { node1, new Methods.Node<int>(5, null) });
//Methods.DepthFirstTraversal(node2);
//Methods.BreadthFirstTraversal(node2);
//Methods.KMP_naive("aaba", "aaaabaabaababaaba");

//Methods.Rod(4, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }, new List<int> { 1, 5, 8, 9, 10, 17, 17, 20 });
//Methods.RodDP(4, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }, new List<int> { 1, 5, 8, 9, 10, 17, 17, 20 });

//value = [20, 5, 10, 40, 15, 25]
//weight = [1, 2, 3, 8, 7, 4]
//int W = 10

//Output: Knapsack value is 60

//value = 20 + 40 = 60
//weight = 1 + 8 = 9 < W
//Methods.KnapsackMoreThan1(10, new List<int> { 1, 2, 3, 8, 7, 4 }, new List<int> { 20, 5, 10, 40, 15, 25 });
//Methods.Knapsack(10, new List<int> { 1, 2, 3, 8, 7, 4 }, new List<int> { 20, 5, 10, 40, 15, 25 });
//Methods.Coins(15, new List<int> { 1, 3, 5, 7 });
//Methods.LongestCommonSubsequenceAll("ABCBDAB", "BDCABA");
//Methods.Boxes(new List<Methods.Box> { new Methods.Box(7, 6, 4), new Methods.Box(3, 2, 1), new Methods.Box(6, 5, 4), new Methods.Box(32, 12, 10) });
//Methods.PalindromeSubsequence("ABBDCACB");
//Methods.Dice(2, 2, 3);
//Methods.Partitions(new List<int> { 3, 1, 1, 2, 2, 1 });
//Methods.WordBreak("Wordbreakproblem", new HashSet<string> { "this", "th", "is", "famous", "Word","break", "b", "r", "e", "a", "k", "br", "bre", "brea", "ak", "problem" });

int[,] a = new int[5, 2] { { 8, 9}, { 10, 6}, { 4, 7}, { 5, 5}, { 9, 6} };
int[,] t = new int[4, 2] { { 2, 2 }, { 3, 1 }, { 1, 2 }, { 3 , 2} };
int[] entryTime = new int[2] { 3, 5 };
int[] exitTime = new int[2] { 2, 1 };

// n = stations
// m = lines
//Methods.AssemblyLine(a, 5, 2, entryTime, exitTime, t);
//Methods.LongestIncreasingSubsequence(new List<int> { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 });
//Methods.Stairs(3, 2);

IList<Methods.Job> jobs = new List<Methods.Job> { new Methods.Job(0, 6, 60), new Methods.Job(1, 4, 30),
    new Methods.Job(3, 5, 10), new Methods.Job(5, 7, 30), new Methods.Job(5, 9, 50), new Methods.Job(7, 8, 10) };

//Methods.WeightedJobs(jobs);
//Methods.MaxProductCutting(5);
Methods.PotsOfGold2(new List<int> { 4, 6, 2, 3 });
//Methods.Jumps(new List<int> {4, 2, 0, 3,2, 0, 1, 8 });
//new List<List<int>> { new List<int> { 0, 20, 30, 100 }, new List<int> { 20, 0, 15, 75 }, new List<int> { 30, 15, 0, 0 }, new List<int> { 100, 75, 50, 0 } }
public static class Methods
{
    public class Node<T>
    {
        public T Value { get; set; }
        public IList<Node<T>> Children { get; set; }

        public Node(T value, IList<Node<T>> children)
        {
            this.Value = value;
            this.Children = children;
        }
    }

    public static void DepthFirstTraversal(Node<int> start)
    {
        var visited = new HashSet<Node<int>>();
        var nextLayer = new Stack<Node<int>>();
        nextLayer.Push(start);

        Node<int> current = null;
        while (0 != nextLayer.Count)
        {
            current = nextLayer.Pop();
            if (!visited.Contains(current))
            {
                Console.WriteLine(current.Value);
                if (null != current.Children)
                {
                    foreach (var x in current.Children)
                    {
                        nextLayer.Push(x);
                    }
                }
            }
            visited.Add(current);
        }
    }

    public static void BreadthFirstTraversal(Node<int> start)
    {
        var visited = new HashSet<Node<int>>();
        var nextLayer = new Stack<Node<int>>();
        nextLayer.Push(start);

        Node<int> current = null;
        while (0 != nextLayer.Count)
        {
            current = nextLayer.Pop();
            if (!visited.Contains(current))
            {
                Console.WriteLine(current.Value);
                if (null != current.Children)
                {
                    foreach (var x in current.Children)
                    {
                        nextLayer.Push(x);
                    }
                }
            }
            visited.Add(current);
        }
    }
    // aaba abaaabaaba
    // Given a text txt[0. . .N - 1] and a pattern pat[0. . .M - 1], write a function 
    // search(char pat[], char txt[]) that prints all occurrences of pat[] in txt[]. You may assume that N > M
    public static void KMP_naive(string pattern, string text)
    {
        for (int i = 0; i < text.Length; ++i)
        {
            if (pattern.Length + i > text.Length)
            {
                break;
            }

            int j = i;
            for (; j < pattern.Length + i; ++j)
            {
                if (text[j] != pattern[j - i])
                {
                    break;
                }
            }
            if (j - i == pattern.Length)
            {
                Console.WriteLine(i);
            }
        }
    }

    public struct WeightMaxInfo
    {
        public int Value;
        public IList<int> Moves;
    }

    // max at weightleft = max(max at weightleft, max(oldWeightLeft - weight) + value) 
    public static void KnapsackMoreThan1(int weight, IList<int> weights, IList<int> values)
    {
        int[,] T = new int[weight, weights.Count];
        WeightMaxInfo[] t = InitializeWeightInfoArray2(weight + 1);
        for (int i = 0; i < weight; ++i)
        {
            for (int j = 0; j < weights.Count; ++j)
            {
                if (weights[j] <= weight - i)
                {
                    int newWeightIndex = i + weights[j];
                    //T[newWeightIndex, j] = T[i,j]
                    if (t[i].Value + values[j] > t[newWeightIndex].Value)
                    {
                        IList<int> moves = new List<int>(t[i].Moves);
                        t[newWeightIndex].Value = t[i].Value + values[j];
                        moves.Add(j);
                        t[newWeightIndex].Moves = moves;
                    }
                }
            }
        }

        WeightMaxInfo max = t[0];
        for (int i = 1; i < t.Length; ++i)
        {
            if (max.Value < t[i].Value)
            {
                max = t[i];
            }
        }

        Console.WriteLine($"Value: {max.Value}");
        foreach (var move in max.Moves)
        {
            Console.WriteLine($"index: {move}, weight: {weights[move]}, value: {values[move]}");
        }
    }

    public class Box
    {
        public int length;
        public int width;
        public int height;

        public Box(int length, int width, int height)
        {
            this.length = length;
            this.width = width;
            this.height = height;
        }

        public IList<Box> AllConfigurations()
        {
            return new List<Box>
            {
                new Box(length, width , height),
                new Box(width, length, height),
                new Box(height, width, length)
            };
        }
    }

    public static void SortBoxes(IList<Box> toSort)
    {
        for (int i = 0; i < toSort.Count; ++i)
        {
            for (int j = 0; j < toSort.Count; ++j)
            {
                if (toSort[i].width * toSort[i].length > toSort[j].width * toSort[j].length)
                {
                    var temp = toSort[j];
                    toSort[j] = toSort[i];
                    toSort[i] = temp;
                }
            }
        }
    }

    public struct BoxesMax
    {
        public int height;
        public IList<Box> moves;
    }

    public static void Boxes(IList<Box> boxes)
    {
        BoxesMax max = new BoxesMax();
        max.height = 0;
        max.moves = new List<Box>();
        IList<Box> allConfigurations = new List<Box>(boxes.SelectMany(x => x.AllConfigurations()));
        for (int i = 0; i < allConfigurations.Count; i++)
        {
            //for (int j = 0; j < allConfigurations.Count; j++)
            {
                Console.WriteLine($"length: {allConfigurations[i].length}, width: {allConfigurations[i].width}");
            }
        }
        Console.WriteLine();
        SortBoxes(allConfigurations);
        for (int i = 0; i < allConfigurations.Count; i++)
        {
            //for (int j = 0; j < allConfigurations.Count; j++)
            {
                Console.WriteLine($"length: {allConfigurations[i].length}, width: {allConfigurations[i].width}");
            }
        }
        BoxesMax[] matrix = new BoxesMax[allConfigurations.Count + 1];
        for (int i = 0; i < allConfigurations.Count; i++)
        {
            //for (int j = 0; j < allConfigurations.Count; j++)
            {
                var newBoxesMax = new BoxesMax();
                newBoxesMax.height = 0;
                newBoxesMax.moves = new List<Box>();
                matrix[i] = newBoxesMax;
            }
        }



        IList<BoxesMax> answer = matrix.Where(x => x.moves != null).ToList();
        max = answer[0];
        for (int i = 1; i < answer.Count; ++i)
        {
            if (answer[i].height > max.height)
            {
                max = answer[i];
            }
        }
        Console.WriteLine($"height: {max.height}");
        Console.WriteLine($"number of boxes: {max.moves.Count}");
        foreach (var move in max.moves)
        {
            Console.WriteLine($"Box: length: {move.length}, width: {move.width}, height: {move.height}");
        }
    }

    public static bool IsPalindrome(string str)
    {
        int halfWay = str.Length / 2;
        for (int i = 0; i < halfWay; i++)
        {
            if (str[i] != str[str.Length - i - 1])
            {
                return false;
            }
        }
        return true;
    }

    public class PalindromeAgg
    {
        public int Value;
        public ISet<string> Palindromes;
    }

    //For example, consider the sequence ABBDCACB.
    //The length of the longest palindromic subsequence is 5
    //The longest palindromic subsequence is BCACB
    public static void PalindromeSubsequence(string str)
    {
        Console.WriteLine(str);
        string strRev = new(str.Reverse().ToArray());
        Console.WriteLine(strRev);
        //IDictionary<int, ISet<string>> maxes = new Dictionary<int, ISet<string>>();
        PalindromeAgg[,] matrix = new PalindromeAgg[str.Length + 1, str.Length + 1];
        for (int i = 0; i < str.Length + 1; ++i)
        {
            //maxes[i] = new HashSet<string>();
            for (int j = 0; j < str.Length + 1; ++j)
            {
                var newAgg = new PalindromeAgg();
                newAgg.Value = 0;
                newAgg.Palindromes = new HashSet<string>();
                matrix[i, j] = newAgg;
            }
        }
        for (int i = 1; i < str.Length + 1; ++i)
        {
            for (int j = 1; j < str.Length + 1; ++j)
            {
                if (str[i - 1] == strRev[j - 1])
                {
                    var pal = new PalindromeAgg();
                    pal.Value = matrix[i - 1, j - 1].Value + 1;
                    pal.Palindromes = new HashSet<string>();
                    if (matrix[i - 1, j - 1].Palindromes.Count == 0)
                    {
                        pal.Palindromes.Add("" + str[i - 1]);
                    }
                    else
                    {
                        foreach (var p in matrix[i - 1, j - 1].Palindromes)
                        {
                            pal.Palindromes.Add(p + str[i - 1]);
                        }
                    }
                    matrix[i, j] = pal;
                }
                else
                {
                    if (matrix[i, j - 1].Value > matrix[i - 1, j].Value)
                    {
                        var pal = new PalindromeAgg();
                        pal.Value = matrix[i, j - 1].Value;
                        pal.Palindromes = new HashSet<string>(matrix[i, j - 1].Palindromes);
                        matrix[i, j] = pal;
                    }
                    else if (matrix[i, j - 1].Value == matrix[i - 1, j].Value)
                    {
                        var pal = new PalindromeAgg();
                        pal.Value = matrix[i, j - 1].Value;
                        pal.Palindromes = new HashSet<string>(matrix[i, j - 1].Palindromes);
                        foreach (var p in matrix[i - 1, j].Palindromes)
                        {
                            pal.Palindromes.Add(p);
                        }
                        matrix[i, j] = pal;
                    }
                    else
                    {
                        var pal = new PalindromeAgg();
                        pal.Value = matrix[i - 1, j].Value;
                        pal.Palindromes = new HashSet<string>(matrix[i, j - 1].Palindromes);
                        matrix[i, j] = pal;
                    }
                }
            }
        }
        // considering letter i going forward 
        /*for (int i = 1; i < str.Length + 1; ++i)
        {
            // j = any letter >= i for reversed string
            for (int j = 1; j < str.Length - i + 1; ++j)
            {
                Console.WriteLine($"{str[i - 1]}, {str[str.Length -j]}");
                if (str[i - 1] == str[str.Length - j - 1])
                {
                    var pal = new PalindromeAgg();
                    pal.Value = matrix[i, j].Value + 1;
                    pal.Palindromes = new HashSet<string>();
                    foreach (var p in matrix[i, j].Palindromes)
                    {
                        pal.Palindromes.Add(p + str[i - 1]);
                    }
                    matrix[i + 1, j + 1] = pal;
                }
                else
                {
                    if (matrix[i, j - 1].Value > matrix[i - 1, j].Value)
                    {
                        var pal = new PalindromeAgg();
                        pal.Value = matrix[i, j - 1].Value;
                        pal.Palindromes = new HashSet<string>(matrix[i, j - 1].Palindromes);
                        matrix[i, j] = pal;
                    }
                    else if (matrix[i, j - 1].Value == matrix[i - 1, j].Value)
                    {
                        var pal = new PalindromeAgg();
                        pal.Value = matrix[i, j - 1].Value;
                        pal.Palindromes = new HashSet<string>(matrix[i, j - 1].Palindromes);
                        foreach (var p in matrix[i - 1, j].Palindromes)
                        {
                            pal.Palindromes.Add(p);
                        }
                        matrix[i, j] = pal;
                    }
                    else
                    {
                        var pal = new PalindromeAgg();
                        pal.Value = matrix[i - 1, j].Value;
                        pal.Palindromes = new HashSet<string>(matrix[i, j - 1].Palindromes);
                        matrix[i, j] = pal;
                    }
                }
            }
        }*/
        Console.WriteLine(matrix[str.Length, str.Length].Value);
        foreach (var p in matrix[str.Length, str.Length].Palindromes)
        {
            Console.WriteLine($"pal: {p}");
        }
    }


    public static void MaxPalindrome(string str)
    {
        string maxPalindromes = "";
        for (int i = 0; i < str.Length; i++)
        {
            for (int j = i + 1; j < str.Length; ++j)
            {
                string sub = str.Substring(i, j);
                if (IsPalindrome(sub))
                {
                    if (maxPalindromes.Length < sub.Length)
                    {
                        maxPalindromes = sub;
                    }
                }
            }
        }
    }

    class LCS
    {
        public int Value;
        public IList<int> Moves;
        public IList<int> Movesj;
        public LCS()
        {
            Value = 0;
            Moves = new List<int>();
            Movesj = new List<int>();
        }

        public LCS(LCS c)
        {
            Value = c.Value;
            Moves = new List<int>(c.Moves);
            Movesj = new List<int>(c.Movesj);
        }
    }

    class LCS2
    {
        public int Value;
        public HashSet<IList<int>> Moves;
        public HashSet<IList<int>> Movesj;
        public LCS2()
        {
            Value = 0;
            Moves = new HashSet<IList<int>>();
            Movesj = new HashSet<IList<int>>();
            Moves.Add(new List<int>());
            Movesj.Add(new List<int>());
        }

        public LCS2(LCS2 c)
        {
            Value = c.Value;
            Moves = new HashSet<IList<int>>(c.Moves.Select(x => new List<int>(x)));
            Movesj = new HashSet<IList<int>>(c.Movesj.Select(x => new List<int>(x)));
        }
    }

    public static int LongestCommonSubsequenceRecInner(string a, string b)
    {
        if (a.Length == 0 || b.Length == 0)
        {
            return 0;
        }
        int max = 0;
        for (int i = 0; i < a.Length; ++i)
        {
            string newStringA = new(a.Skip(i + 1).ToArray());
            for (int j = 0; j < b.Length; ++j)
            {
                string newStringB = new(b.Skip(j + 1).ToArray());
                int result = LongestCommonSubsequenceRecInner(newStringA, newStringB) + (a[i] == b[j] ? 1 : 0);
                if (result > max)
                {
                    max = result;
                }
            }
        }
        return max;
    }

    public static void LongestCommonSubsequenceRec(string a, string b)
    {
        Console.WriteLine(LongestCommonSubsequenceRecInner(a, b));
    }

    public static void LongestCommonSubsequenceAll(string a, string b)
    {
        Dictionary<int, IList<string>> sequences = new Dictionary<int, IList<string>>();
        LCS2[,] matrix = new LCS2[a.Length + 1, b.Length + 1];
        for (int i = 0; i < a.Length; ++i)
        {
            for (int j = 0; j < b.Length; ++j)
            {
                matrix[i, j] = new LCS2();
            }
        }

        for (int i = 1; i < a.Length + 1; ++i)
        {
            for (int j = 1; j < b.Length + 1; ++j)
            {
                if (a[i - 1] == b[j - 1])
                {
                    var newLCS = new LCS2(matrix[i - 1, j - 1]);
                    foreach (var moves in newLCS.Moves)
                    {
                        moves.Add(i - 1);
                    }
                    foreach (var movesj in newLCS.Movesj)
                    {
                        movesj.Add(j - 1);
                    }
                    ++newLCS.Value;
                    int count = newLCS.Moves.First().Count;
                    if (!sequences.ContainsKey(count))
                    {
                        sequences[count] = new List<string>();
                    }
                    foreach (var moves in newLCS.Moves)
                    {
                        string str = "";
                        foreach (var move in moves)
                        {
                            str += a[move];
                        }
                        sequences[count].Add(str);
                    }
                    matrix[i, j] = newLCS;
                }
                else
                {
                    if (matrix[i - 1, j].Value > matrix[i, j - 1].Value)
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }
                    else if (matrix[i - 1, j].Value == matrix[i, j - 1].Value)
                    {
                        matrix[i, j] = new LCS2(matrix[i - 1, j]);
                        // we have a split, tabulate both ways
                        foreach (var moves in matrix[i, j - 1].Moves)
                        {
                            matrix[i, j].Moves.Add(moves);
                        }

                        foreach (var movesj in matrix[i, j - 1].Movesj)
                        {
                            matrix[i, j].Movesj.Add(movesj);
                        }
                    }
                    else
                    {
                        matrix[i, j] = matrix[i, j - 1];
                    }
                }
            }
        }
        for (int i = a.Length; i >= 0; --i)
        {
            if (sequences.ContainsKey(i))
            {
                foreach (var lcs in sequences[i])
                {
                    Console.WriteLine(lcs);
                }
                break;
            }
        }
    }

    public static void LongestCommonSubsequence1(string a, string b)
    {
        LCS[,] matrix = new LCS[a.Length + 1, b.Length + 1];
        for (int i = 0; i < a.Length + 1; ++i)
        {
            for (int j = 0; j < b.Length + 1; ++j)
            {
                matrix[i, j] = new LCS();
            }
        }

        for (int i = 1; i < a.Length + 1; ++i)
        {
            for (int j = 1; j < b.Length + 1; ++j)
            {
                if (matrix[i - 1, j].Value > matrix[i, j].Value)
                {
                    matrix[i, j] = new LCS(matrix[i - 1, j]);
                }
                if (matrix[i, j - 1].Value > matrix[i, j].Value)
                {
                    matrix[i, j] = new LCS(matrix[i, j - 1]);
                }
                if (matrix[i - 1, j - 1].Value > matrix[i, j].Value)
                {
                    matrix[i, j] = new LCS(matrix[i - 1, j - 1]);
                }

                if (a[i - 1] == b[j - 1] && !matrix[i, j].Moves.Contains(i) && !matrix[i, j].Movesj.Contains(j))
                {
                    matrix[i, j] = new LCS(matrix[i, j]);
                    matrix[i, j].Moves.Add(i);
                    matrix[i, j].Movesj.Add(j);
                    matrix[i, j].Value++;
                }
            }
        }
        Console.WriteLine(matrix[a.Length, b.Length].Value);
        Console.WriteLine(matrix[a.Length, b.Length].Moves.Count);
        foreach (var move in matrix[a.Length, b.Length].Moves)
        {
            Console.WriteLine(a[move - 1]);
        }

    }

    public static WeightMaxInfo KnapsackRec(int weight, IList<int> weights, IList<int> values, WeightMaxInfo agg)
    {
        WeightMaxInfo max = new WeightMaxInfo();
        bool choiceCanBeMan = false;
        for (int i = 0; i < weights.Count; ++i)
        {
            if (weights[i] <= weight)
            {
                choiceCanBeMan = true;
                IList<int> newWeights = new List<int>();
                IList<int> newValues = new List<int>();
                for (int j = 0; j < values.Count; ++j)
                {
                    if (j != i)
                    {
                        newWeights.Add(weights[j]);
                        newValues.Add(values[j]);
                    }
                }
                IList<int> moves = new List<int>(agg.Moves);
                WeightMaxInfo newAgg = new WeightMaxInfo();
                newAgg.Value = agg.Value + values[i];
                moves.Add(values[i]);
                newAgg.Moves = moves;
                WeightMaxInfo maxInner = KnapsackRec(weight - weights[i], newWeights, newValues, newAgg);
                if (maxInner.Value > max.Value)
                {
                    max = maxInner;
                }
            }
        }

        if (!choiceCanBeMan)
        {
            return agg;
        }
        return max;
    }

    public static void Knapsack(int weight, IList<int> weights, IList<int> values)
    {
        WeightMaxInfo agg = new WeightMaxInfo();
        agg.Moves = new List<int>();
        agg = KnapsackRec(weight, weights, values, agg);
        Console.WriteLine($"value: {agg.Value}");
        foreach (var move in agg.Moves)
        {
            Console.WriteLine(move);
        }
    }

    public static void Knapsack2(int weight, IList<int> weights, IList<int> values)
    {

    }

    public struct LengthMaxInfo
    {
        public int Value;
        public IList<int> Moves;
    }

    // coins:
    // For example, consider S = { 1, 3, 5, 7 }.
    // If the desired change is 15, the minimum number of coins required is 3
    public static void Coins(int changeRequired, IList<int> coins)
    {
        // maxes will be considered in the following way:
        // index = how much change is left
        LengthMaxInfo[] mins = InitializeLengthMaxInfoArrayForMin(changeRequired);
        for (int i = changeRequired; i >= 0; i--)
        {
            for (int j = 0; j < coins.Count; ++j)
            {
                if (coins[j] <= i)
                {
                    if (mins[i - coins[j]].Value > mins[i].Value + 1)
                    {
                        LengthMaxInfo newMax = new LengthMaxInfo();
                        newMax.Moves = new List<int>(mins[i].Moves);
                        newMax.Moves.Add(j);
                        newMax.Value = mins[i].Value + 1;
                        mins[i - coins[j]] = newMax;
                    }
                }
            }
        }

        if (mins[0].Moves.Count != 0)
        {
            Console.WriteLine($"found min: {mins[0].Value}, {mins[0].Moves.Count}");
            foreach (var move in mins[0].Moves)
            {
                Console.WriteLine($"move: {coins[move]}");
            }
        }
        else
        {
            Console.WriteLine("did not find min");
        }
    }

    // Given a rod of length n and a list of rod prices of length i, where 1 <= i <= n,
    // find the optimal way to cut the rod into smaller rods to maximize profit.
    // Input: length[] = [1, 2, 3, 4, 5, 6, 7, 8]
    // price[] = [1, 5, 8, 9, 10, 17, 17, 20]

    // Rod length: 4
    // Best: Cut the rod into two pieces of length 2 each to gain revenue of 5 + 5 = 10
    public static void RodDP(int rodLength, IList<int> lengths, IList<int> prices)
    {
        var lengthMaxes = InitializeLengthMaxInfoArray(rodLength);
        LengthMaxInfo[,] agg = InitializeAgg(rodLength, lengths.Count);
        for (int i = lengthMaxes.Length - 1; i >= 0; --i)
        {
            for (int j = 0; j < lengths.Count; ++j)
            {
                if (lengths[j] <= i)
                {
                    int valueAttainedWithMove = lengthMaxes[i].Value + prices[j];

                    int newLengthAttainedWithMove = i - lengths[j];
                    if (valueAttainedWithMove > agg[i, j].Value)
                    {
                        var newLengthMaxInfo = new LengthMaxInfo();
                        var newMoves = new List<int>(lengthMaxes[i].Moves);
                        newMoves.Add(j);
                        newLengthMaxInfo.Value = valueAttainedWithMove;
                        newLengthMaxInfo.Moves = newMoves;
                        agg[i, j] = newLengthMaxInfo;
                    }

                    if (agg[i, j].Value > lengthMaxes[newLengthAttainedWithMove].Value)
                    {
                        lengthMaxes[newLengthAttainedWithMove] = agg[i, j];
                    }
                }
                else
                {
                    var dummy = new LengthMaxInfo();
                    dummy.Value = -1;
                    agg[i, j] = dummy;
                }
            }
        }

        LengthMaxInfo max = lengthMaxes[0];
        for (int i = 1; i < lengthMaxes.Length; ++i)
        {
            if (max.Value < lengthMaxes[i].Value)
            {
                max = lengthMaxes[i];
            }
        }

        Console.WriteLine($"Value: {max.Value}");
        foreach (var move in max.Moves)
        {
            Console.WriteLine($"Move: {move}");
        }
    }

    static public LengthMaxInfo[,] InitializeAgg(int rodLength, int lengthsCount)
    {
        LengthMaxInfo[,] agg = new LengthMaxInfo[rodLength + 1, lengthsCount];
        for (int i = 0; i < rodLength + 1; ++i)
        {
            for (int j = 0; j < lengthsCount; ++j)
            {
                var lengthMaxInfo = new LengthMaxInfo();
                lengthMaxInfo.Value = 0;
                lengthMaxInfo.Moves = new List<int>();
                agg[i, j] = lengthMaxInfo;
            }
        }
        return agg;
    }

    static public WeightMaxInfo[,] InitializeAgg2(int rodLength, int lengthsCount)
    {
        WeightMaxInfo[,] agg = new WeightMaxInfo[rodLength + 1, lengthsCount];
        for (int i = 0; i < rodLength + 1; ++i)
        {
            for (int j = 0; j < lengthsCount; ++j)
            {
                var lengthMaxInfo = new WeightMaxInfo();
                lengthMaxInfo.Value = i == 0 ? rodLength : 0;
                //lengthMaxInfo.WeightLeft = i == 0 ? rodLength : int.MaxValue;
                lengthMaxInfo.Moves = new List<int>();
                agg[i, j] = lengthMaxInfo;
            }
        }
        return agg;
    }

    static public LengthMaxInfo[] InitializeLengthMaxInfoArray(int size)
    {
        LengthMaxInfo[] agg = new LengthMaxInfo[size + 1];
        for (int i = 0; i < size + 1; ++i)
        {
            var lengthMaxInfo = new LengthMaxInfo();
            lengthMaxInfo.Value = 0;
            lengthMaxInfo.Moves = new List<int>();
            agg[i] = lengthMaxInfo;
        }
        return agg;
    }

    static public LengthMaxInfo[] InitializeLengthMaxInfoArrayForMin(int size)
    {
        LengthMaxInfo[] agg = new LengthMaxInfo[size + 1];
        for (int i = 0; i < size + 1; ++i)
        {
            var lengthMaxInfo = new LengthMaxInfo();
            lengthMaxInfo.Value = i == size ? 0 : int.MaxValue;
            lengthMaxInfo.Moves = new List<int>();
            agg[i] = lengthMaxInfo;
        }
        return agg;
    }

    public static WeightMaxInfo[] InitializeWeightInfoArray2(int size)
    {
        WeightMaxInfo[] agg = new WeightMaxInfo[size];
        for (int i = 0; i < size; ++i)
        {
            var lengthMaxInfo = new WeightMaxInfo();
            lengthMaxInfo.Value = 0;
            //lengthMaxInfo.WeightLeft = int.MaxValue;
            lengthMaxInfo.Moves = new List<int>();
            agg[i] = lengthMaxInfo;
        }
        return agg;
    }

    public static void Rod(int rodLength, IList<int> lengths, IList<int> prices)
    {
        IList<int> answer = RodRecursiveInner(rodLength, lengths, prices, new List<int>());
        for (int i = 0; i < answer.Count; ++i)
        {
            Console.WriteLine(answer[i]);
        }
    }

    static IList<int> RodRecursiveInner(int rodLength, IList<int> lengths, IList<int> prices, IList<int> tempAnswer)
    {
        IList<int> maxAnswer = new List<int>();
        bool terminate = true;
        for (int i = 0; i < lengths.Count; ++i)
        {
            if (lengths[i] <= rodLength)
            {
                IList<int> newTempAnswer = new List<int>(tempAnswer);
                newTempAnswer.Add(i);
                IList<int> answer = RodRecursiveInner(rodLength - lengths[i], lengths, prices, newTempAnswer);
                maxAnswer = CalculateMax(answer, maxAnswer, prices);
                terminate = false;
            }
        }
        if (terminate)
        {
            return tempAnswer;
        }
        return maxAnswer;
    }

    static IList<int> CalculateMax(IList<int> a, IList<int> b, IList<int> prices)
    {
        return CalculateValue(a, prices) > CalculateValue(b, prices) ? a : b;
    }

    static int CalculateValue(IList<int> x, IList<int> prices)
    {
        int agg = 0;
        for (int i = 0; i < x.Count; ++i)
        {
            agg += prices[x[i]];
        }
        return agg;
    }

    public class DiceAgg
    {
        public int NumberOfSums;
        public ISet<IList<int>> Faces;
    }

    public static void Dice(int d, int f, int s)
    {
        DiceAgg[,] matrix = new DiceAgg[s + 1, d + 1];
        for (int i = 0; i < s + 1; ++i)
        {
            for (int j = 0; j < d + 1; ++j)
            {
                var agg = new DiceAgg();
                agg.Faces = new HashSet<IList<int>>();
                if (j == 0)
                {
                    agg.NumberOfSums = 1;
                    var list = new List<int>();
                    //list.Add(j);
                    agg.Faces.Add(list);
                }
                else
                {
                    agg.NumberOfSums = 0;
                }

                matrix[i, j] = agg;
            }
        }

        // at sum i
        for (int i = 0; i < s + 1; ++i)
        {
            // at die j
            for (int j = 1; j < d + 1; ++j)
            {
                // every face
                for (int k = 1; k < f + 1; ++k)
                {
                    if (i >= k)
                    {
                        //int added = matrix[i - k, j - 1].NumberOfSums + matrix[i, j].NumberOfSums;
                        //matrix[i, j].NumberOfSums = added;
                        foreach (var face in matrix[i - k, j - 1].Faces)
                        {
                            var newList = new List<int>(face);
                            newList.Add(k);
                            if (newList.Sum() == i)
                            {
                                Console.WriteLine($"array from {i - k},{j - 1} for {i}, {j}");
                                foreach (var n in newList)
                                {
                                    Console.Write($"{n} ");
                                }
                                Console.WriteLine();
                                Console.WriteLine("array end");
                                matrix[i, j].Faces.Add(newList);
                            }
                        }
                    }
                }
            }
        }

        var answer = matrix[s, d];
        Console.WriteLine($"number of sums: {answer.NumberOfSums}");
        foreach (var face in answer.Faces)
        {
            Console.WriteLine("{");
            foreach (int answerF in face)
            {
                Console.Write($"{answerF} ");
            }
            Console.WriteLine();
            Console.WriteLine("}");
        }
    }

    public class Tower
    {
        public IList<int> Tiles;
        public IList<int> Ks;
        public void Print()
        {
            Console.WriteLine("tower start");
            foreach (var tile in Tiles)
            {
                Console.WriteLine(tile);
            }
            Console.WriteLine("tower stop");
        }
    }

    public class TowerAgg
    {
        public ISet<Tower> Towers;
    }

    public static void TowerFunc(IList<int> tiles, int n, int k)
    {
        TowerAgg[,] agg = new TowerAgg[tiles.Count, n + 1];
        for (int i = 0; i < tiles.Count; ++i)
        {
            for (int j = 0; i < n; ++j)
            {
                agg[i, j].Towers = new HashSet<Tower>();
                /*foreach (var x in tiles)
                {
                    agg[i, j].Ks.Add(k);
                }*/
            }
        }

        // sort tiles
        for (int i = 0; i < tiles.Count; ++i)
        {
            for (int j = 0; j < tiles.Count; ++j)
            {
                if (tiles[i] < tiles[j])
                {
                    tiles[i] ^= tiles[j];
                    tiles[j] ^= tiles[i];
                    tiles[i] ^= tiles[j];
                }
            }
        }


        for (int i = 0; i < tiles.Count; ++i)
        {
            for (int j = 1; j < n; ++j)
            {
                bool found = false;
                TowerAgg towerAgg = new TowerAgg();
                towerAgg.Towers = new HashSet<Tower>();
                for (int h = i; h >= 0; --h)
                {
                    foreach (var t in agg[h, j - 1].Towers)
                    {
                        if (t.Ks[i] > 0)
                        {
                            Tower tower = new Tower();
                            tower.Tiles = new List<int>(t.Tiles);
                            tower.Tiles.Add(tiles[i]);
                            tower.Ks = new List<int>(t.Ks);
                            --tower.Ks[i];
                            towerAgg.Towers.Add(tower);
                            found = true;
                        }
                    }
                }
                if (found)
                {
                    agg[i, j] = towerAgg;
                }
                else
                {
                    agg[i, j] = null;
                }
                // agg[i, j] = agg[every k smaller than i, j - 1]
            }
        }

        for (int i = 0; i < tiles.Count; ++i)
        {
            foreach (var tower in agg[i, n - 1].Towers)
            {
                tower.Print();
            }
        }
    }

    public class MultAgg
    {
        public int Value;
        public IList<bool> Captured;
        public Tuple<int, int> Dimensions;
        public IList<Tuple<int, int>> Moves;
    }

    // min operation at parenthesis n and possible capture m = capture at m * min at dimensions of n - 1 where m is not already captured
    public static void MatrixMult(IList<Tuple<int, int>> dimensions)
    {
        int possibleCaptures = dimensions.Count() - 1;
        int possibleParenthesis = dimensions.Count - 2;
        MultAgg[,] matrix = new MultAgg[possibleParenthesis, possibleCaptures];
        // initialize

        for (int i = 1; i < possibleParenthesis; ++i)
        {
            for (int j = 0; j < possibleCaptures; ++j)
            {
                for (int k = 0; k < possibleCaptures; ++k)
                {
                    MultAgg multAgg = matrix[i - 1, k];
                    if (!multAgg.Captured[j])
                    {
                        int multiplyBy = j > k ? dimensions[j + 1].Item2 : dimensions[j + 1].Item1;
                        int newValue = multiplyBy * matrix[i - 1, k].Value;
                        if (newValue > matrix[i, j].Value)
                        {
                            MultAgg newMultAgg = new MultAgg();
                            newMultAgg.Value = newValue;
                            int left = j > k ? multAgg.Dimensions.Item1 : dimensions[j + 1].Item1;
                            int right = j > k ? dimensions[j + 1].Item2 : multAgg.Dimensions.Item2;
                            newMultAgg.Dimensions = new Tuple<int, int>(left, right);
                            newMultAgg.Captured = new List<bool>(multAgg.Captured);
                            newMultAgg.Captured[j] = true;
                            matrix[i, j] = newMultAgg;
                        }
                    }
                }
            }
        }
    }


    public class PAgg
    {
        public ISet<IList<int>> sets;
    }
    //Consider S = { 3, 1, 1, 2, 2, 1 }


    //We can partition S into two partitions, each having a sum of 5.

    //S1 = {1, 1, 1, 2}
    //S2 = {2, 3}

    //Note that this solution is not unique.Here’s another solution.

    //S1 = { 3, 1, 1}
    //S2 = {2, 2, 1}

    // 
    public static void Partitions(IList<int> toPartition)
    {
        int half = toPartition.Sum() / 2;
        Console.WriteLine(half);
        PAgg[] matrix = new PAgg[half + 1];
        for (int i = 0; i < half + 1; ++i)
        {
            matrix[i] = new PAgg();
            matrix[i].sets = new HashSet<IList<int>>();
        }
        for (int i = 0; i < half + 1; ++i)
        {
            for (int j = 0; j < toPartition.Count; ++j)
            {
                if (i + toPartition[j] <= half)
                {
                    var pAggToAddTo = matrix[i + toPartition[j]];
                    var pAgg = matrix[i];
                    if (pAgg.sets.Any())
                    {
                        foreach (var set in pAgg.sets)
                        {
                            var newList = new List<int>(set);
                            if (!newList.Contains(j))
                            {
                                newList.Add(j);
                                pAggToAddTo.sets.Add(newList);
                            }
                        }
                    }
                    else
                    {
                        var l = new List<int>();
                        l.Add(j);
                        pAggToAddTo.sets.Add(l);
                    }
                }
            }
        }

        var answer = matrix[half];
        foreach (var set in answer.sets)
        {
            var toRemoveFrom = new List<int>(toPartition);
            foreach (var x in set)
            {
                toRemoveFrom.Remove(toPartition[x]);
            }

            Console.WriteLine($"first partition start");
            foreach (var x in set)
            {
                Console.WriteLine($"element: {toPartition[x]}");
            }
            Console.WriteLine($"first partition end");
            Console.WriteLine();

            Console.WriteLine($"second partition start");
            foreach (var x in toRemoveFrom)
            {
                Console.WriteLine($"element: {x}");
            }
            Console.WriteLine($"second partition end");
            Console.WriteLine();
        }
    }

    public class WordBreakAgg
    {
        public ISet<IList<string>> Separations;
    }

    // Word Break Problem: Given a string and a dictionary of words, determine if the string can be segmented into a
    // space-separated sequence of one or more dictionary words.

    // For example,

    // Input:


    // dict[] = { this, th, is, famous, Word, break, b, r, e, a, k, br, bre, brea, ak, problem };

    // word = Wordbreakproblem

    // Output:


    // Word b r e a k problem
    // Word b r e ak problem
    // Word br e a k problem
    // Word br e ak problem
    // Word bre a k problem
    // Word bre ak problem
    // Word brea k problem

    // splice at [i, j] where j > i = is splice a word && is substring before splice [0, i - 1] separable
    public static void WordBreak(string toCheck, ISet<string> words)
    {
        // initialize
        WordBreakAgg[] matrix = new WordBreakAgg[words.Count];
        for (int i = 0; i < words.Count; ++i)
        {
            var wordBreakingAgg = new WordBreakAgg();
            wordBreakingAgg.Separations = new HashSet<IList<string>>();
            matrix[i] = wordBreakingAgg;
        }

        for (int i = 0; i < words.Count; ++i)
        {
            for (int j = i + 1; j < words.Count + 1; ++j)
            {
                string splice = toCheck.Substring(i, j - i);
                if (words.Contains(splice) && 0 == i)
                {
                    var temp = new List<string> { splice };
                    matrix[j - 1].Separations.Add(temp);
                }
                else if (words.Contains(splice) && matrix[i - 1].Separations.Any())
                {
                    foreach (var x in matrix[i - 1].Separations)
                    {
                        var temp = new List<string>(x);
                        temp.Add(splice);
                        matrix[j - 1].Separations.Add(temp);
                    }
                }
            }
        }

        foreach (var x in matrix.Last().Separations)
        {
            Console.WriteLine("start");
            foreach (var y in x)
            {
                Console.WriteLine(y);
            }
            Console.WriteLine("end");
            Console.WriteLine();
        }
    }

    // X: ABCBDAB
    // Y: BDCABA

    // The length of the LCS is 4
    // LCS are BDAB, BCAB, and BCBA

    // longest lcs at i and j = a[i] == a[j] then i - 1, j - 1 otherwise max([i - 1, j], [i, j - 1])
    public class LCS3AGG
    {
        public ISet<string> Set;
    }

    public static void LCS3(string a, string b)
    {
        LCS3AGG[,] matrix = new LCS3AGG[a.Length + 1, b.Length + 1];
        for (int i = 1; i < a.Length + 1; i++)
        {
            for (int j = 1; i < b.Length + 1; j++)
            {
                if (a[i - 1] == b[j - 1])
                {
                    var agg = new LCS3AGG();
                    agg.Set = new HashSet<string>();
                    foreach (var x in matrix[i - 1, j - 1].Set)
                    {
                        agg.Set.Add(x + a[i - 1]);
                    }
                    matrix[i, j] = agg;
                }
                else
                {
                    var agg = new LCS3AGG();

                    var x = matrix[i - 1, j];
                    var y = matrix[i, j - 1];
                    var xCount = x.Set.Any() ? x.Set.First().Length : 0;
                    var yCount = y.Set.Any() ? y.Set.First().Length : 0;
                    if (xCount > yCount)
                    {
                        agg.Set = new HashSet<string>(x.Set);
                    }
                    else if (xCount == yCount)
                    {
                        agg.Set = new HashSet<string>(y.Set);
                    }
                    else
                    {
                        agg.Set = new HashSet<string>(x.Set);
                        foreach (var y1 in y.Set)
                        {
                            agg.Set.Add(y1);
                        }
                    }
                }
            }
        }
    }

    public class AssemblyLineAgg
    {
        public int Time;
        public IList<Tuple<int, int>> Moves;
    }

    // min time S i j = min(foreach i S [i, j - 1 ] + switchTime[i, j - 1] if i != outer i) + timeRequired[i, j]
    public static void AssemblyLine(int[,] timeRequired, int n, int m, int[] enterTime, int[] exitTime, int[,] switchTime)
    {
        AssemblyLineAgg[,] stations = new AssemblyLineAgg[n, m];
        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < m; ++j)
            {
                AssemblyLineAgg agg = new AssemblyLineAgg();
                agg.Time = int.MaxValue;
                agg.Moves = new List<Tuple<int, int>>();
                stations[i, j] = agg;
            }
        }

        for (int i = 0; i < m; ++i)
        {
            timeRequired[m - 1, i] += exitTime[i];
            stations[0, i].Time = enterTime[i] + timeRequired[0, i];
        }

        for (int i = 1; i < n; ++i)
        {
            for (int j = 0; j < m; ++j)
            {
                AssemblyLineAgg minTime = new AssemblyLineAgg();
                minTime.Time = int.MaxValue;
                for (int k = 0; k < m; ++k)
                {
                    AssemblyLineAgg check = new AssemblyLineAgg();

                    if (j != k)
                    {
                        check.Time = stations[i - 1, k].Time + switchTime[i - 1, k];
                    }
                    else
                    {
                        check.Time = stations[i - 1, k].Time;
                    }
                    check.Moves = new List<Tuple<int, int>>(stations[i - 1, k].Moves);
                    check.Moves.Add(new Tuple<int, int>(i - 1, k));
                    if (check.Time < minTime.Time)
                    {
                        minTime = check;
                    }
                }
                minTime.Time += timeRequired[i, j];
                stations[i, j] = minTime;
            }
        }

        AssemblyLineAgg answer = new AssemblyLineAgg();
        answer.Time = int.MaxValue;
        for (int i = 0; i < m; ++i)
        {
            AssemblyLineAgg check = stations[n - 1, i];
            if (check.Time < answer.Time)
            {
                answer = check;
            }
        }

        Console.WriteLine($"Time: {answer.Time}");

        Console.WriteLine("Moves start");
        foreach (var move in answer.Moves)
        {
            Console.WriteLine($"move {move.Item1}, {move.Item2}");
        }
        Console.WriteLine("Moves end");
    }

    // string at ith removal is palindrome = (any i - 1 th removal) - removal of character c that has not already been removed
    public static void PalindromeK(string toCheck, int k)
    {
        int[,] matrix = new int[toCheck.Length + 1, toCheck.Length + 1];
        for (int i = 1; i < toCheck.Length + 1; ++i)
        {
            for (int j = toCheck.Length; j >= 0; --j)
            {
                if (toCheck[i - 1] == toCheck[j - 1])
                {
                    matrix[i, j] = matrix[i - 1, j - 1];
                }
                else
                {
                    matrix[i, j] = (matrix[i - 1, j] < matrix[i, j - 1] ? matrix[i - 1, j] : matrix[i, j - 1]) + 1;
                }
            }
        }

        Console.WriteLine($"{k >= matrix[toCheck.Length, toCheck.Length]}");
    }

    public class LISAgg
    {
        public int Value;
        public ISet<IList<int>> Moves;
    }

    // long increasing subsequence at index i = max(any j before it j where < i) otherwise 0
    public static void LongestIncreasingSubsequence(IList<int> list)
    {
        LISAgg[] agg = new LISAgg[list.Count];
        for (int i = 0; i < list.Count; ++i)
        {
            agg[i] = new LISAgg();
            agg[i].Moves = new HashSet<IList<int>>();
        }

        for (int i = 1; i < list.Count; ++i)
        {
            bool found = false;
            LISAgg max = new LISAgg();
            max.Value = int.MinValue;
            max.Moves = new HashSet<IList<int>>();
            for (int j = 0; j < i; ++j)
            {

                if (list[i] > list[j] && agg[j].Value + 1 > max.Value)
                {
                    max.Moves.Clear();
                    found = true;
                    max.Value = agg[j].Value + 1;
                    if (agg[j].Moves.Count == 0)
                    {
                        var newMoves = new List<int>();
                        newMoves.Add(list[j]);
                        newMoves.Add(list[i]);
                        max.Moves.Add(newMoves);
                    }
                    else
                    {
                        foreach (var moves in agg[j].Moves)
                        {
                            var newMoves = new List<int>(moves);
                            newMoves.Add(list[i]);
                            max.Moves.Add(newMoves);
                        }
                    }
                }

            }
            if (!found)
            {
                max.Value = 0;
            }
            agg[i] = max;
        }
        int k = 0;
        Console.WriteLine("start agg");
        foreach (var a in agg)
        {
            Console.WriteLine($"start {k}");
            foreach (var moves in a.Moves)
            {
                Console.WriteLine("moves start");
                foreach (var move in moves)
                {
                    Console.WriteLine(move);
                }
                Console.WriteLine("moves end");
            }
            Console.WriteLine("end");
            ++k;
        }
        Console.WriteLine("end agg");
    }

    // number of ways to climb stair n = sum of number of ways to get to stair n - i for all i <= m
    public class StairsAgg
    {
        public HashSet<IList<int>> Steps;
    }
    public static void Stairs(int n, int m)
    {
        StairsAgg[] stairsAgg = new StairsAgg[n + 1];
        for (int i = 0; i < n + 1; ++i)
        {
            stairsAgg[i] = new StairsAgg();
            stairsAgg[i].Steps = new HashSet<IList<int>>();
        }

        stairsAgg[0].Steps.Add(new List<int>());

        for (int i = 1; i < n + 1; ++i)
        {
            for (int j = 1; j < m + 1; ++j)
            {
                if (i < j)
                {
                    break;
                }

                foreach (var s in stairsAgg[i - j].Steps)
                {
                    var list = new List<int>(s);
                    list.Add(j);
                    stairsAgg[i].Steps.Add(list);
                }
            }
        }

        foreach (var s in stairsAgg.Last().Steps)
        {
            Console.WriteLine("steps start");
            foreach (var x in s)
            {
                Console.WriteLine($"{x}");
            }
            Console.WriteLine("steps end");
        }
    }

    // max at job i = max (job at job j for every job j < i where j does not overlap with i) 
    public class Job
    {
        public int StartTime;
        public int EndTime;
        public int Profit;

        public Job(int startTime, int endTime, int profit)
        {
            StartTime = startTime;
            EndTime = endTime;
            Profit = profit;
        }
    }

    public class JobAgg
    {
        public int Profit;
        public ISet<IList<Job>> JobsSet;
    }

    public static void WeightedJobs(IList<Job> jobs)
    {
        for (int i = 0; i < jobs.Count; ++i)
        {
            for (int j = 0; j < jobs.Count; ++j)
            {
                Job jobI = jobs[i];
                Job jobJ = jobs[j];
                if (jobJ.StartTime > jobI.StartTime)
                {
                    jobs[i] = jobJ;
                    jobs[j] = jobI;
                }
            }
        }

        JobAgg[] agg = new JobAgg[jobs.Count];
        agg[0] = new JobAgg();
        agg[0].Profit = jobs[0].Profit;
        agg[0].JobsSet = new HashSet<IList<Job>>();
        agg[0].JobsSet.Add(new List<Job> { jobs[0] });

        for (int i = 1; i < jobs.Count; ++i)
        {
            bool found = false;
            Job jobI = jobs[i];
            JobAgg max = new JobAgg();
            max.Profit = int.MinValue;
            max.JobsSet = new HashSet<IList<Job>>();
            for (int j = 0; j < i; ++j)
            {
                Job jobJ = jobs[j];
                if (jobI.StartTime >= jobJ.EndTime)
                {
                    found = true;
                    if (agg[j].Profit > max.Profit)
                    {
                        max.Profit = agg[j].Profit;
                        max.JobsSet.Clear();

                        foreach (var js in agg[j].JobsSet)
                        {
                            var list = new List<Job>(js);
                            list.Add(jobI);
                            max.JobsSet.Add(list);
                        }
                    }
                    else if (agg[j].Profit == max.Profit)
                    {
                        foreach (var js in agg[j].JobsSet)
                        {
                            var list = new List<Job>(js);
                            list.Add(jobI);
                            max.JobsSet.Add(list);
                        }
                    }
                }
            }
            if (!found)
            {
                max.Profit = jobI.Profit;
                var list = new List<Job>();
                list.Add(jobI);
                max.JobsSet.Add(list);
            }
            else
            {
                max.Profit += jobI.Profit;
            }

            agg[i] = max;
        }

        JobAgg max2 = agg[0];
        for (int i = 1; i < agg.Length; ++i)
        {
            if (agg[i].Profit > max2.Profit)
            {
                max2 = agg[i];
            }
        }

        foreach (var jbs in max2.JobsSet)
        {
            Console.WriteLine("jobs start");
            Console.WriteLine($"Total Profit {max2.Profit}");
            foreach (var job in jbs)
            {
                Console.WriteLine($"Start: {job.StartTime}, End: {job.EndTime}, Profit: {job.Profit}");
            }
            Console.WriteLine("jobs end");
        }
    }

    public class PotsOfGoldAgg
    {
        public int Value;
        public ISet<IList<int>> Moves;
        public int Choice;
        public int MaxPassThrough;
    }

    public static void SetMoveRight(PotsOfGoldAgg[,] matrix, IList<int> potsOfGold, int i, int j)
    {
        int check = 0;
        int move = 0;
        if (j - 2 >= 1)
        {
            check = matrix[i, j - 2].Value + potsOfGold[j];
        }

        var value = new PotsOfGoldAgg();
        if (i + 1 <= j - 1 && matrix[i + 1, j - 1].Value + potsOfGold[i] > check)
        {

            value.Value = matrix[i + 1, j - 1].Value + potsOfGold[i];
            value.Moves = new HashSet<IList<int>>(matrix[i + 1, j - 1].Moves);
            matrix[i, j] = value;
            move = i;
        }
        else if (j - 2 >= i)
        {
            value.Value = check;
            value.Moves = new HashSet<IList<int>>(matrix[i, j - 2].Moves);
            matrix[i, j - 2] = value;
            move = j;
        }
        else
        {
            IList<int> list = null;
            value.Moves = new HashSet<IList<int>>();
            if (potsOfGold[i] > potsOfGold[j])
            {
                value.Value = potsOfGold[i];
                list = new List<int> { i };
            }
            else
            {
                value.Value = potsOfGold[j];
                list = new List<int> { j };
            }
            value.Moves.Add(list);
            matrix[i, j] = value;
            return;
        }

        foreach (var moves in value.Moves)
        {
            moves.Add(move);
        }

        matrix[i, j] = value;
    }

    public static void SetMoveLeft(PotsOfGoldAgg[,] matrix, IList<int> potsOfGold, int i, int j)
    {
        int check = 0;
        int move = 0;
        if (i + 2 <= j)
        {
            check = matrix[i + 2, j].Value + potsOfGold[i];
        }

        var value = new PotsOfGoldAgg();
        if (j - 1 >= i + 1 && matrix[i + 1, j - 1].Value + potsOfGold[j] > check)
        {

            value.Value = matrix[i + 1, j - 1].Value + potsOfGold[j];
            value.Moves = new HashSet<IList<int>>(matrix[i + 1, j - 1].Moves);
            matrix[i, j] = value;
            move = j;
        }
        else if (i + 2 <= j)
        {
            value.Value = check;
            value.Moves = new HashSet<IList<int>>(matrix[i + 2, j].Moves);
            matrix[i, j] = value;
            move = i;
        }
        else
        {
            IList<int> list = null;
            value.Moves = new HashSet<IList<int>>();
            if (potsOfGold[i] > potsOfGold[j])
            {
                value.Value = potsOfGold[i];
                list = new List<int> { i };
            }
            else
            {
                value.Value = potsOfGold[j];
                list = new List<int> { j };
            }
            value.Moves.Add(list);
            matrix[i, j] = value;
            return;
        }

        foreach (var moves in value.Moves)
        {
            moves.Add(move);
        }
        matrix[i, j] = value;
    }

    public static void PotsOfGold3(IList<int> potsOfGold)
    {

        // i = starting index, j = length of subarray
        PotsOfGoldAgg[,] matrix = new PotsOfGoldAgg[potsOfGold.Count, potsOfGold.Count + 1];
        for (int i = 0; i < potsOfGold.Count; ++i)
        {
            var value = new PotsOfGoldAgg();
            value.Value = potsOfGold[i];
            value.Moves = new HashSet<IList<int>>();
            var list = new List<int> { i };
            matrix[i, 0] = value;
        }

        for (int i = 0; i < potsOfGold.Count; ++i)
        {
            for (int j = 1; j < potsOfGold.Count + 1; ++j)
            {
                PotsOfGoldAgg prevRight = null;
                PotsOfGoldAgg prevLeft = null;
                PotsOfGoldAgg answer = null;
                PotsOfGoldAgg newAgg = new PotsOfGoldAgg();
                newAgg.Moves = new HashSet<IList<int>>();
                int newValue = 0;
                int choice = 0;
                int maxPassThrough = 0;
                if (i == 0 && j == 4)
                { 
                }
                if (i + j <= potsOfGold.Count)
                {
                    prevRight = matrix[i, j - 1];
                    prevLeft = matrix[i + 1, j - 1];
                }

                if (null == prevRight)
                {
                    continue;
                }
                else if (null == prevLeft)
                {
                    answer = prevRight;
                    newValue = (prevRight.Value - prevRight.Choice) + potsOfGold[i];
                    choice = potsOfGold[i];
                    maxPassThrough = prevRight.Value;
                }
                else
                {
                    
                    if (prevRight.MaxPassThrough + potsOfGold[i] > prevLeft.MaxPassThrough + potsOfGold[i + j - 1])
                    {
                        answer = prevRight;
                        newValue = prevRight.MaxPassThrough + potsOfGold[i];
                        choice = potsOfGold[i];
                        maxPassThrough = prevRight.Value;
                    }
                    else
                    {
                        answer = prevLeft;
                        newValue = prevLeft.MaxPassThrough + potsOfGold[i + j - 1];
                        choice = potsOfGold[i + j - 1];
                        maxPassThrough = prevLeft.Value;
                    }                    
                }

                newAgg.Value = newValue;
                newAgg.Choice = choice;
                newAgg.MaxPassThrough = maxPassThrough;
                foreach (var move in answer.Moves)
                {
                    var innerList = new List<int>(move);
                    innerList.Add(choice);
                    newAgg.Moves.Add(innerList);
                }

                matrix[i, j] = newAgg;
            }
        }

        Console.WriteLine($"{matrix[0, potsOfGold.Count].Value}");
        foreach (var moves in matrix[0, potsOfGold.Count].Moves)
        {
            foreach (var move in moves)
            {
                Console.WriteLine($"{move}");
            }
        }
        Console.WriteLine("end");
    }

    public static void PotsOfGold2(IList<int> potsOfGold)
    {
        Debugger.Break();
        PotsOfGoldAgg[,] matrix = new PotsOfGoldAgg[potsOfGold.Count, potsOfGold.Count];
        for (int i = 0; i < potsOfGold.Count; ++i)
        {
            var value = new PotsOfGoldAgg();
            value.Value = potsOfGold[i];
            value.Moves = new HashSet<IList<int>>();
            var list = new List<int> { i };
            matrix[i, i] = value;
        }

        for (int i = 0; i < potsOfGold.Count; ++i)
        {
            for (int j = i + 1; j < potsOfGold.Count; ++j)
            {
                if (matrix[i + 1, j].Value + potsOfGold[i] > matrix[i, j - 1].Value + potsOfGold[j])
                {
                    SetMoveLeft(matrix, potsOfGold, i, j);
                }
                else
                {
                    SetMoveRight(matrix, potsOfGold, i, j);
                }
            }
        }

        Console.WriteLine($"{matrix[potsOfGold.Count - 1, potsOfGold.Count - 1].Value}");
        foreach (var moves in matrix[potsOfGold.Count - 1, potsOfGold.Count - 1].Moves)
        {
            foreach (var move in moves)
            {
                Console.WriteLine($"{move}");
            }
        }
        Console.WriteLine("end");
    }

    public static void PotsOfGold(IList<int> potsOfGold)
    {
        PotsOfGoldAgg[,] matrix = new PotsOfGoldAgg[potsOfGold.Count, potsOfGold.Count];
        for (int i = 0; i < potsOfGold.Count; ++i)
        {
            var value = new PotsOfGoldAgg();
            value.Value = potsOfGold[i];
            value.Moves = new HashSet<IList<int>>();
            var list = new List<int> { i };
            matrix[i, i] = value;
        }

        for (int i = 0; i < potsOfGold.Count; ++i)
        {
            for (int j = i + 1; j < potsOfGold.Count; ++j)
            {
                if (matrix[i + 1, j].Value + potsOfGold[i] > matrix[i, j - 1].Value + potsOfGold[j])
                {
                    var value = new PotsOfGoldAgg();
                    value.Value = matrix[i + 1, j].Value + potsOfGold[i];
                    value.Moves = new HashSet<IList<int>>(matrix[i + 1, j].Moves);
                    foreach (var moves in value.Moves)
                    {
                        moves.Add(i);
                    }
                    matrix[i, j] = value;
                }
                else
                {
                    var value = new PotsOfGoldAgg();
                    value.Value = matrix[i, j - 1].Value + potsOfGold[j];
                    value.Moves = new HashSet<IList<int>>(matrix[i, j - 1].Moves);
                    foreach (var moves in value.Moves)
                    {
                        moves.Add(j);
                    }
                    matrix[i, j] = value;
                }
            }
        }
    }

    public class CuttingAgg
    {
        public int Product;
        public ISet<IList<int>> Set;
    }
    public static void MaxProductCutting(int length)
    {
        CuttingAgg[] array = new CuttingAgg[length];

        for (int i = 0; i < length; ++i)
        {
            array[i] = new CuttingAgg();
            array[i].Set = new HashSet<IList<int>>();
        }

        array[0].Product = 1;
        var x = new List<int> { 1 };
        array[0].Set = new HashSet<IList<int>>();
        array[0].Set.Add(x);

        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j <= i; ++j)
            {
                CuttingAgg agg = array[i];
                int extra = (i == j ? 1 : i - (i - j));
                if (agg.Product < array[i - j].Product * extra)
                {
                    agg.Product = array[i - j].Product * extra;
                    agg.Set = new HashSet<IList<int>>();
                    foreach (var moves in array[i - j].Set)
                    {
                        IList<int> list = new List<int>(moves);
                        list.Add(extra);
                        agg.Set.Add(list);
                    }
                    array[i] = agg;
                }
            }
        }

        Console.WriteLine($"moves start {array.Last().Product}");
        foreach (var moves in array.Last().Set)
        {
            foreach(var move in moves)
            {
                Console.WriteLine($"{move}");
            }
        }
        Console.WriteLine("moves end");
    }


    public class FlightAgg
    {
        public int Value;
        public ISet<IList<int>> Moves;
    }

   /* public static void FastestFlight(int[,] flights)
    {
        FlightAgg[] agg = new FlightAgg[flights.Length];
        for (int i = 0; i < flights.Length; ++i)
        {
            for (int j = 0; j < flights.Length; ++j)
            {
                agg[i, j] = new FlightAgg();
                agg[i, j].Moves = new HashSet<IList<int>>();
            }
        }

        for (int i = 0; i < flights.Length; ++i)
        {
            for (int j = 0; j < flights.Length; ++j)
            {
                
            }
        }
    }*/

    public class JumpsAgg
    {
        public int Value;
        public ISet<IList<int>> Moves;
    }

    public static void Jumps(IList<int> nums)
    {
        JumpsAgg[] agg = new JumpsAgg[nums.Count];
        for (int i = 0; i < nums.Count; ++i)
        {
            
            agg[i] = new JumpsAgg();
            agg[i].Value = 0;
            agg[i].Moves = new HashSet<IList<int>>();
            agg[i].Moves.Add(new List<int>());
        }

        for (int i = 0; i < nums.Count; ++i)
        {
            JumpsAgg min = null;
            for(int j = 0; j < i; ++j)
            {
                if (nums[j] >= i - j && (null == min || min.Value > agg[j].Value + 1))
                {
                    bool minWasNull = null == min;
                    min = new JumpsAgg();
                    
                    min.Moves = new HashSet<IList<int>>();
                    foreach (var x in agg[j].Moves)
                    {
                        var list = new List<int>(x);
                        
                        if (list.Count == 0)
                        {
                            list.Add(j);
                        }
                        list.Add(i);
                        min.Moves.Add(list);
                    }

                    min.Value = agg[j].Value + 1;
                }
            }
            if (null != min)
            {
                agg[i] = min;
            }
        }

        Console.WriteLine($"value {agg.Last().Value}");
        foreach (var moves in agg.Last().Moves)
        {
            Console.WriteLine("start");
            foreach (var move in moves)
            {
                Console.WriteLine($"move {move}");
            }
            Console.WriteLine("end");
        }
    }
}