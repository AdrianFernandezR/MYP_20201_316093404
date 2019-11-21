#include <stdio.h>

void quicksort(int arrayToSort[],int under,int uper)
{
    int first,last,firstValueArr,auxLast;

    if(under<uper)
    {
        first=under;
        last=uper;
        firstValueArr=arrayToSort[under];

        while(first<last)
        {
            
            while(firstValueArr>=arrayToSort[first] && first<uper)
            {
            first=first+1;
            }
            
            while(firstValueArr<arrayToSort[last])
            {
            last=last-1;
            }

            if(first<last)
            {
                auxLast=arrayToSort[first];
                arrayToSort[first]=arrayToSort[last];
                arrayToSort[last]=auxLast;
            }
        }
        
        auxLast=arrayToSort[under];
        arrayToSort[under]=arrayToSort[last];
        arrayToSort[last]=auxLast;
        
        quicksort(arrayToSort,under,last-1);
        quicksort(arrayToSort,last+1,uper);
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
    
    quicksort(arrToSort,0,arrLength-1);
    
    for(first=0;first<arrLength;first++)
    {
        
        printf(" %d", arrToSort[first]);
    }
    
    return 0;
}
