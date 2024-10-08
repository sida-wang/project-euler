from decimal import *

target = 10^18
expectationSumLower = Decimal(1)
expectationSumHigher = Decimal(2)

for i in range(3,target - 3):
    expectationSumLower, expectationSumHigher = expectationSumHigher, expectationSumHigher + 1 + 2 * expectationSumLower / i

expectationKnights = 1 + 1 + 2 * expectationSumLower / (target - 3)

emptyFraction = 1 - (expectationKnights / target)

print(emptyFraction)

'''
E(1-X) = 1-E(X)
Instead of computing portion of empty chairs, we look to compute the portion of knights and then take the complement
Rather than compute the portion immediately, let K be the numbers of knights sat around the table. 
We consider E(N*) defined to be the expected value of K for N* chairs.

Now, by fixing the one knight, the problem can be converted from a round table problem to a long table problem by taking away 3 chairs.
Hence E(N*) = 1 + E((N-3)**), where E(N**) is defined to be the expected value of K for N** chairs in a long table. 

Consider the case of N** = 4, diagramatically as O O O O. There is a 1/4 probability of positioning the next knight in any of the chairs.
In the first option, K O O O, the chair to the right of the knight must be excluded i.e. K X O O.
Hence the number of knights in this configuration is 1 + E(2**)
For the second option, X K X O, the number of knights is 1 + E(1**)
Similarly, the third and fourth options yield 1 + E(1**) and 1 + E(2**) respectively. 

If we define S(N**) to be the sum for i = 1 to N of E(i**), we can show that E(N**) = 1 + 1/N**(2S((N-2)**))

E(N) = 1 - E(1-N) = 1 - (1/N)E(N*) = 1 - (1/N)E((N-3)**) which can be solved using the equation above. 
'''
