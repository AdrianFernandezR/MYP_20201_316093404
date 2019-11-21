#include <stdio.h>
#include <omp.h>

void auxiliarQuicksort(int *array, int left, int right, int limit) 
{
    
    int i = left;
    int j = right;
    int auxSize;
    int pivot = array[(left + right) / 2];
    
    {
        while (i <= j) 
        {
            while (array[i] < pivot)
            {
                i++;
            }
        
            while (array[j] > pivot)
            {
                j--;
            }
            
            if (i <= j) 
            {
                auxSize = array[i];
                array[i] = array[j];
                array[j] = auxSize;
                i++;
                j--;
            }
        }
    }


    if  ((right-left)<limit)
    {
        if (left < j){ auxiliarQuicksort(array, left, j, limit); }           
        if (i < right){ auxiliarQuicksort(array, i, right, limit); }

    }
    else
    {
        #pragma omp task    
        { auxiliarQuicksort(array, left, j, limit); }
        #pragma omp task    
        { auxiliarQuicksort(array, i, right, limit); }       
    }

}

void quicksortParallel(int *array, int lenArray, int numThreads){

    int limit = 1000;

    #pragma omp parallel num_threads(numThreads)
    {   
        #pragma omp single nowait
        {
            auxiliarQuicksort(array, 0, lenArray-1, limit);  
        }
    }   

}




int main()
{
    int arrLength, first;
    scanf("%d", &arrLength);
    
    int arrToSort[arrLength];
    
    for (first = 0; first < arrLength; first++)
    {
        scanf("%d", &arrToSort[first]);
    }
    
    quicksortParallel(arrToSort, arrLength, 2);
    
    for(first=0; first < arrLength; first++)
    {
        
        printf("%d", arrToSort[first]);
        printf(" ");

    }
    
    printf("\n");
}
