/*
To get answer for part 2 just modify the input file by hand
The input file should be in the same directory as the executable and named input.txt
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

char* readFromFile() 
{
    FILE* file = fopen("input.txt", "r");

    if (file == NULL) 
    {
        printf("Failed to open the file.\n");
        return NULL;
    }

    fseek(file, 0, SEEK_END);

    long fileSize = ftell(file);

    fseek(file, 0, SEEK_SET);

    char* fileContent = (char*)malloc(fileSize + 1);

    if (fileContent == NULL) 
    {
        printf("Failed to allocate memory.\n");

        fclose(file);

        return NULL;
    }

    fread(fileContent, 1, fileSize, file);

    fileContent[fileSize] = '\0';

    fclose(file);

    return fileContent;
}

typedef struct 
{
    long long time;
    long long distance;
} Race;

Race* processFileContent(char* content, int* numRaces) 
{
    int i = 0;

    char* lines[2];
    char* line = strtok(content, "\n");

    while (line != NULL)
    {
        lines[i++] = line;
        line = strtok(NULL, "\n");
    }

    long long* time = malloc(sizeof(long long) * i);
    long long* distance = malloc(sizeof(long long) * i);

    i = 0;

    char* token = strtok(lines[0], " ");

    token = strtok(NULL, " ");

    while (token != NULL)
    {
        sscanf(token, "%lld", &time[i]);

        token = strtok(NULL, " ");

        i++;
    }

    *numRaces = i;

    i = 0;

    token = strtok(lines[1], " ");

    token = strtok(NULL, " ");

    while (token != NULL)
    {
        sscanf(token, "%lld", &distance[i]);

        token = strtok(NULL, " ");

        i++;
    }

    Race* races = (Race*)malloc(*numRaces * sizeof(Race));

    for (i = 0; i < *numRaces; i++)
    {
        races[i].time = time[i];
        races[i].distance = distance[i];
    }

    free(time);
    free(distance);

    return races;
}

bool raceIsWinnable(Race race, int holdTime)
{
    long long distance = (race.time - holdTime) * holdTime;

    return distance > race.distance;
}

int main() 
{
    char* content = readFromFile();

    if (content == NULL) 
    {
        return 1;
    }

    int numRaces;

    Race* races = processFileContent(content, &numRaces);

    free(content);

    long long totalProduct = 1;

    for (int i = 0; i < numRaces; i++)
    {
        printf("\nRace %d\n", i + 1);

        printf("Time: %lld, Distance %lld\n", races[i].time, races[i].distance);

        int winnableWays = 0;

        for (int holdTime = 0; holdTime <= races[i].time; holdTime++)
        {
            if (raceIsWinnable(races[i], holdTime))
            {
                winnableWays++;
            }
        }

        printf("Race is winnable in %d ways\n", winnableWays);
        
        totalProduct *= winnableWays;
    }

    printf("\nTotal winnable ways: %lld\n", totalProduct);

    free(races);

    return 0;
}