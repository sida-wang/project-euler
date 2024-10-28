using System;

Int64 totalSum = 0;
int digitLim = 16;
for (int d = 2; d <= digitLim; d++) {
Int64 dMin = (Int64)Math.Pow(10, d-1);
Int64 dMax = (Int64)Math.Pow(10, d);

int minK = (int)Math.Ceiling(Math.Log(Math.Pow(10, d-1)/d, 9));
int maxK = (int)Math.Floor(Math.Log(Math.Pow(10, d)/d, 2));

Int64 dSum = 0;

void dfsHelper(int maxIndex, Int64 sum,  int remaining, Int64[] arr, Dictionary<int, int> intDict, Dictionary<int, Int64> sumDict) {
    if (sum >= dMax) {
        return;
    }

    if (maxIndex == 0) {
        if (intDict.ContainsKey(0)) {
            intDict[0] += remaining;
        } else {
            intDict[0] = remaining;
        }
        dSum += checker(sum + 1, intDict, sumDict);
        dSum += checker(sum - 1, intDict, sumDict);
        return;
    }
    
    if (remaining == 0) {
        dSum += checker(sum + 1, intDict, sumDict);
        dSum += checker(sum - 1, intDict, sumDict);
        return;
    }
    
    for (int i = maxIndex; i >= 0; i--) {
        Dictionary<int, int> newIntDict = new(intDict);
        Dictionary<int, Int64> newSumDict = new(sumDict);
        if (newIntDict.ContainsKey(i)) {
            newIntDict[i]++;
            newSumDict[i] += arr[i];
        } else {
            newIntDict.Add(i, 1);
            newSumDict.Add(i, arr[i]);
        }
        dfsHelper(i, sum + arr[i], remaining-1, arr, newIntDict, newSumDict);
    }
    
}

Int64 checker(Int64 sum, Dictionary<int, int> intDict, Dictionary<int,Int64> sumDict) {
    if (sum < dMin) {
        return 0;
    }
    Int64 originalSum = sum;
    while (sum > 0) {
        sum = Math.DivRem(sum, 10, out Int64 digit);
        if (intDict.TryGetValue((int)digit, out int value)) {
            if (value == 0) {
                return 0;
            } else {
                intDict[(int)digit]--;
            }
        } else {
            return 0;
        }
    }
    Console.WriteLine($"Found solution in {originalSum}");
    Int64 intSum = 0;
    for (int i = 0; i < 10; i++) {
        if (sumDict.TryGetValue(i, out Int64 value)) {
        // Console.WriteLine($"{i}: {value}");
        intSum += value;
        }
    }
    Console.WriteLine($"Checking sum obtained: {intSum}");
    return originalSum;
}

for (int k = minK; k <= maxK ; k++) {
    Int64[] arr = new Int64[10];
    int maxIndex = 9;
    for (int j = 0; j < 10; j++) {
        if (Math.Pow(j, k) >= dMax) {
            maxIndex = j-1;
            break;
        }
        arr[j] = (Int64)Math.Pow(j, k);
        // Console.WriteLine(arr[j]);
    }
    
    dfsHelper(maxIndex, 0, d, arr, [], []);
}

Console.WriteLine($"S*({d}) = {dSum}");
totalSum += dSum;
// Console.WriteLine(totalSum);

}

Console.WriteLine($"S({digitLim}) = {totalSum}");
