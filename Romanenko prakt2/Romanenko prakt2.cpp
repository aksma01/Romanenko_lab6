#include <iostream>
#include <vector>
#include <queue>
#include <cstdlib>
#include <ctime>

using namespace std;

struct Process {
    int id;           
    int arrival_time;  
    int burst_time;   
    int remaining_time; 
    int priority;      
    int start_time;    
    int completion_time; 
    int waiting_time;   

    Process(int id, int arrival, int burst, int priority)
        : id(id), arrival_time(arrival), burst_time(burst), remaining_time(burst),
        priority(priority), start_time(-1), completion_time(-1), waiting_time(0) {}
};

void generate_processes(vector<Process>& processes, int num_processes) {
    srand(time(0));
    for (int i = 0; i < num_processes; ++i) {
        int arrival_time = rand() % 10;      
        int burst_time = rand() % 10 + 1;    
        int priority = rand() % 5 + 1;       
        processes.push_back(Process(i + 1, arrival_time, burst_time, priority));
    }
}

void round_robin(vector<Process>& processes, int time_quantum) {
    queue<Process*> ready_queue;
    int current_time = 0;

    for (auto& process : processes) {
        if (process.arrival_time <= current_time) {
            ready_queue.push(&process);
        }
    }

    while (!ready_queue.empty()) {
        Process* current_process = ready_queue.front();
        ready_queue.pop();

        if (current_process->remaining_time > time_quantum) {
            current_process->remaining_time -= time_quantum;
            current_time += time_quantum;
            ready_queue.push(current_process);
        }
        else {
            current_time += current_process->remaining_time;
            current_process->completion_time = current_time;
            current_process->remaining_time = 0;
        }

        cout << "Process " << current_process->id << " remaining time: " << current_process->remaining_time << endl;
    }
}

void fcfs(vector<Process>& processes) {
    int current_time = 0;
    for (auto& process : processes) {
        process.start_time = max(current_time, process.arrival_time);
        current_time = process.start_time + process.burst_time;
        process.completion_time = current_time;
        process.waiting_time = process.start_time - process.arrival_time;

        cout << "Process " << process.id << " started at: " << process.start_time
            << ", completed at: " << process.completion_time
            << ", waiting time: " << process.waiting_time << endl;
    }
}

void priority_scheduling(vector<Process>& processes) {
    sort(processes.begin(), processes.end(), [](const Process& a, const Process& b) {
        return a.priority > b.priority;
        });

    int current_time = 0;
    for (auto& process : processes) {
        process.start_time = max(current_time, process.arrival_time);
        current_time = process.start_time + process.burst_time;
        process.completion_time = current_time;
        process.waiting_time = process.start_time - process.arrival_time;

        cout << "Process " << process.id << " started at: " << process.start_time
            << ", completed at: " << process.completion_time
            << ", priority: " << process.priority
            << ", waiting time: " << process.waiting_time << endl;
    }
}

void calculate_average_times(const vector<Process>& processes) {
    int total_waiting_time = 0;
    int total_turnaround_time = 0;

    for (const auto& process : processes) {
        total_waiting_time += process.waiting_time;
        total_turnaround_time += (process.completion_time - process.arrival_time);
    }

    double avg_waiting_time = (double)total_waiting_time / processes.size();
    double avg_turnaround_time = (double)total_turnaround_time / processes.size();

    cout << "Average waiting time: " << avg_waiting_time << endl;
    cout << "Average turnaround time: " << avg_turnaround_time << endl;
}

int main() {
    int num_processes = 5;  
    int time_quantum = 3;    

    vector<Process> processes;
    generate_processes(processes, num_processes);

    cout << "Round Robin Scheduling:" << endl;
    round_robin(processes, time_quantum);

    cout << "\nFCFS Scheduling:" << endl;
    fcfs(processes);

    cout << "\nPriority Scheduling:" << endl;
    priority_scheduling(processes);

    cout << "\nAverage Times for Round Robin:" << endl;
    calculate_average_times(processes);

    return 0;
}
