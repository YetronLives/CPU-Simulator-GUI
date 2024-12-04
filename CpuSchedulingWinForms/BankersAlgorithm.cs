using System;

class BankersAlgorithm
{
    static void Main()
    {
        int[,] allocation = {
            {0, 1, 0},
            {2, 0, 0},
            {3, 0, 2},
            {2, 1, 1},
            {0, 0, 2}
        };

        int[,] maximum = {
            {7, 5, 3},
            {3, 2, 2},
            {9, 0, 2},
            {2, 2, 2},
            {4, 3, 3}
        };

        int[] available = {3, 3, 2};

        if (IsSafeState(allocation, maximum, available, out int[] safeSequence))
        {
            Console.WriteLine("System is in a safe state.");
            Console.WriteLine("Safe sequence: " + string.Join(" -> ", safeSequence));
        }
        else
        {
            Console.WriteLine("System is in a deadlock state.");
        }
    }

    static bool IsSafeState(int[,] allocation, int[,] maximum, int[] available, out int[] safeSequence)
    {
        int numProcesses = allocation.GetLength(0);
        int numResources = available.Length;

        // Calculate the Need matrix
        int[,] need = new int[numProcesses, numResources];
        for (int i = 0; i < numProcesses; i++)
        {
            for (int j = 0; j < numResources; j++)
            {
                need[i, j] = maximum[i, j] - allocation[i, j];
            }
        }

        // Initialize work and finish vectors
        int[] work = new int[numResources];
        Array.Copy(available, work, numResources);

        bool[] finish = new bool[numProcesses];
        safeSequence = new int[numProcesses];
        int count = 0;

        while (count < numProcesses)
        {
            bool progressMade = false;

            for (int i = 0; i < numProcesses; i++)
            {
                if (!finish[i])
                {
                    bool canAllocate = true;
                    for (int j = 0; j < numResources; j++)
                    {
                        if (need[i, j] > work[j])
                        {
                            canAllocate = false;
                            break;
                        }
                    }

                    if (canAllocate)
                    {
                        // Allocate resources and mark process as finished
                        for (int j = 0; j < numResources; j++)
                        {
                            work[j] += allocation[i, j];
                        }
                        finish[i] = true;
                        safeSequence[count++] = i;
                        progressMade = true;
                    }
                }
            }

            if (!progressMade)
            {
                // No progress made in this iteration, system is in deadlock
                return false;
            }
        }

        return true;
    }
}
