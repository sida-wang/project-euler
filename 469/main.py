from decimal import *

target = 10^18
expectationSumLower = Decimal(1)
expectationSumHigher = Decimal(2)

for i in range(3,target - 3):
    expectationSumLower, expectationSumHigher = expectationSumHigher, expectationSumHigher + 1 + 2 * expectationSumLower / i

expectationKnights = 1 + 1 + 2 * expectationSumLower / (target - 3)

emptyFraction = 1 - (expectationKnights / target)

print(emptyFraction)
