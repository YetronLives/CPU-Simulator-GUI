using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] bp = new double[np];
            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    bp[num] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        //MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                string waitTimeText = "";
                string turnaroundTimeText = "";
                for (num = 0; num <= np - 1; num++)
                {
                    waitTimeText += "P" + (num + 1) + ": " + wtp[num] +"\n";

                }

                DataGridView dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                dgv.Columns.Add("Process", "Process");
                dgv.Columns.Add("Arrival Time", "Arrival Time");
                dgv.Columns.Add("Burst Time", "Burst Time");
                dgv.Columns.Add("Wait Time", "Wait Time");
                dgv.Columns.Add("Completion Time", "Completion Time");
                dgv.Columns.Add("Turnaround Time", "Turnaround Time");

                for (int i = 0; i < np; i++)
                {
                    dgv.Rows.Add("P" + (i + 1),i, bp[i], wtp[i], (wtp[i] + bp[i]), ((wtp[i] + bp[i])-i));
                }
                dgv.Rows.Add("", "", "", "", "", "");
                dgv.Rows.Add("", "", "", "", "Avg. Waiting Time", awt);
                Form resultsForm = new Form
                {
                    Text = "FCFS Scheduling Results",
                    Width = 700,
                    Height = 400
                };
                resultsForm.Controls.Add(dgv);
                resultsForm.ShowDialog();
                // MessageBox.Show("\tSummary\n"+ "Wait Time:\t\n"+waitTimeText);
                // MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();
            }
            else
            {
                //this.Hide();
            }
        }

        
        public static void roundRobinAlgorithm(string userInput)
{
    int np = Convert.ToInt32(userInput);
    int i, counter = 0;
    double total = 0.0;
    double timeQuantum;
    double waitTime = 0, turnaroundTime = 0;
    double averageWaitTime, averageTurnaroundTime;
    double[] arrivalTime = new double[np];
    double[] burstTime = new double[np];
    double[] remainingTime = new double[np];
    double[] completionTime = new double[np];
    double[] waitingTimeArr = new double[np];
    double[] turnaroundTimeArr = new double[np];
    
    int x = np;

    DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

    if (result == DialogResult.Yes)
    {
        for (i = 0; i < np; i++)
        {
            string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ", "Arrival time for P" + (i + 1), "", -1, -1);
            arrivalTime[i] = Convert.ToDouble(arrivalInput);

            string burstInput = Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ", "Burst time for P" + (i + 1), "", -1, -1);
            burstTime[i] = Convert.ToDouble(burstInput);

            remainingTime[i] = burstTime[i];
        }

        string timeQuantumInput = Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum", "", -1, -1);
        timeQuantum = Convert.ToDouble(timeQuantumInput);

        for (total = 0, i = 0; x != 0;)
        {
            if (remainingTime[i] > 0 && arrivalTime[i] <= total)
            {
                if (remainingTime[i] <= timeQuantum)
                {
                    total += remainingTime[i];
                    remainingTime[i] = 0;
                    counter = 1;
                }
                else
                {
                    remainingTime[i] -= timeQuantum;
                    total += timeQuantum;
                }

                if (remainingTime[i] == 0 && counter == 1)
                {
                    x--;
                    completionTime[i] = total;
                    turnaroundTimeArr[i] = completionTime[i] - arrivalTime[i];
                    waitingTimeArr[i] = turnaroundTimeArr[i] - burstTime[i];

                    turnaroundTime += turnaroundTimeArr[i];
                    waitTime += waitingTimeArr[i];
                    counter = 0;
                }
            }

            i = (i + 1) % np; 
        }

        averageWaitTime = waitTime / np;
        averageTurnaroundTime = turnaroundTime / np;
        
        DataGridView dgv = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };

        dgv.Columns.Add("Process", "Process");
        dgv.Columns.Add("Arrival Time", "Arrival Time");
        dgv.Columns.Add("Burst Time", "Burst Time");
        dgv.Columns.Add("Wait Time", "Wait Time");
        dgv.Columns.Add("Completion Time", "Completion Time");
        dgv.Columns.Add("Turnaround Time", "Turnaround Time");

        for (int index = 0; index < np; index++)
        {
            dgv.Rows.Add("P" + (index + 1), arrivalTime[index], burstTime[index],
                         waitingTimeArr[index], completionTime[index], turnaroundTimeArr[index]);
        }

        dgv.Rows.Add("", "", "", "", "", "");
        dgv.Rows.Add("", "", "", "", "Avg. Waiting Time(s)", averageWaitTime.ToString("F2"));
        dgv.Rows.Add("", "", "", "", "Avg. Turnaround Time(s)", averageTurnaroundTime.ToString("F2"));
        Form resultsForm = new Form
        {
            Text = "Round Robin Scheduling Results",
            Width = 700,
            Height = 400
        };
        resultsForm.Controls.Add(dgv);
        resultsForm.ShowDialog();
    }
}
    }
}

