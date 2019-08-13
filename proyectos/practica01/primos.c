#include<stdio.h>

int esprimo(int numero)
{
	if(numero==1) return 0;

	for(int i=2; i<numero; i++){

		if(numero%i==0) return 0;
	}
	return 1;
}

int main()
{
	int n;
	int*a=&n;
	scanf("%i",a);
	for(int i=2; i<n; i++){

		if(esprimo(i)) printf("%i\n", i);
	}


}