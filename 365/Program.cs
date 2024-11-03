using Euler;

Int64 n = (Int64)Math.Pow(10, 18);
Int64 k = (Int64)Math.Pow(10, 9);
Int64 nmk = n - k;

Int64[] remArr = new Int64[Q365.primes.Length];

computeRemainders();
// printRemainders();

solve();
// verify();

void verify()
{
    long prime = 1009;
    long count = 0;
    long kFact = 1;
    for (long i = 1; i <= k; i++)
    {
        long temp = i;
        while (temp % prime == 0)
        {
            temp /= prime;
            count++;
        }
        kFact *= temp % prime;
        kFact %= prime;
    }
    Console.WriteLine(kFact);
    long nFact = 1;
    for (long i = n; i > n - k; i--)
    {
        long temp = i;
        while (temp % prime == 0)
        {
            temp /= prime;
            count--;
        }
        nFact *= temp % prime;
        nFact %= prime;
    }
    long final = nFact * invert(kFact, prime) % prime;
    Console.WriteLine(nFact);
    Console.WriteLine($"coef of 109 should be {final}");
}

void computeRemainders()
{
    for (int i = 0; i < Q365.primes.Length; i++)
    {
        Int64 prime = Q365.primes[i];
        long workingPrime = prime;
        bool skip = false;
        while (workingPrime <= n)
        // for (int j = 0; j < 2; j++)
        {
            long diff = (long)(n / workingPrime) - (long)(nmk / workingPrime) - (long)(k / workingPrime);
            if (diff > 0)
            {
                // Console.WriteLine($"prime {prime} breaking at workingPrime {workingPrime}");
                skip = true;
                break;
            }
            workingPrime *= prime;
        }
        if (skip) {
                remArr[i] = 0;
                continue;
        }

        remArr[i] = factMod(prime);
    }
}

void printRemainders()
{
    for (int i = 0; i < Q365.primes.Length; i++)
    {
        Console.WriteLine($"Remainder of coef mod {Q365.primes[i]} is {remArr[i]}");
    }
}

long subFactMod(long n, long mod)
{
    long fact = 1;
    for (long i = 2; i <= n; i++)
    {
        fact *= i;
        fact %= mod;
    }
    return fact;
}

long rangeFactMod(long a, long b, long m)
{
    int power = (int)Math.Log(b, m);
    return rangeFactModWithPow(a, b, m, power);
}

long rangeFactModWithPow(long a, long b, long m, int power)
{
    long raisedMod = (long)Math.Pow(m, power);
    // Console.WriteLine($"{a}, {b}, {m}, {power}, {raisedMod}");
    long aMindex = (long)Math.Ceiling((decimal)a / raisedMod) - 1;
    long bMindex = b / raisedMod;
    if (bMindex == aMindex)
    {
        return rangeFactModWithPow(a, b, m, power - 1);
    }
    long fact = 1;
    for (long i = aMindex + 1; i <= bMindex; i++)
    {
        fact *= i % m;
        fact %= m;
    }
    if (power == 0)
    {
        return fact;
    }
    if (bMindex - aMindex % 2 == 0)
    {
        fact *= -1; // Contribution from interior (m-1)! mod m = -1;
    }
    fact *= rangeFactModWithPow(a, (aMindex + 1) * raisedMod - 1, m, power - 1) % m;
    fact %= m;
    fact *= rangeFactModWithPow(bMindex * raisedMod + 1, b, m, power - 1) % m;
    fact %= m;
    return fact;
}

Int64 factMod(Int64 m)
{
    Int64 nFactmkFact = rangeFactMod(nmk + 1, n, m);
    long kFact = rangeFactMod(1, k, m);
    Int64 kFactInv = invert(kFact, m);
    // Console.WriteLine($"factMod {kFact}, {kFactInv}, {nFactmkFact}");
    return nFactmkFact * kFactInv % m;
}

Int64 invert(Int64 num, Int64 m)
{
    for (Int64 i = 1; i < m; i++)
    {
        if (num * i % m == 1)
        {
            return i;
        }
    }
    Console.WriteLine($"Error inverting {num} mod {m}");
    return -1;
}

void solve()
{
    long sum = 0;
    for (int rIndex = Q365.primes.Length - 1; rIndex >= 0; rIndex--)
    {
        long r = Q365.primes[rIndex];
        for (int qIndex = rIndex - 1; qIndex >= 0; qIndex--)
        {
            long q = Q365.primes[qIndex];
            (long k1, long k2) = extendedEuclideanAlgorithm(q, r);
            long newRem = q * (k1 * (remArr[rIndex] - remArr[qIndex]) % r) + remArr[qIndex];
            long newMod = q * r;
            // Console.WriteLine($"qr = {newMod} with remainder {newRem}");
            for (int pIndex = qIndex - 1; pIndex >= 0; pIndex--)
            {
                long p = Q365.primes[pIndex];
                // Solve Diophantine equation for pqr
                (long k3, long k4) = extendedEuclideanAlgorithm(p, newMod);
                long anotherRem = p * (k3 * (newRem - remArr[pIndex]) % newMod) + remArr[pIndex];
                if (anotherRem < 0)
                {
                    anotherRem += p * newMod;
                }
                sum += anotherRem;
                // Console.WriteLine($"{anotherRem} with trio [{p},{q},{r}]");
            }
        }
    }
    Console.WriteLine(sum);
}

(long, long) extendedEuclideanAlgorithm(long a, long b)
{
    (long old_r, long r) = (a, b);
    (long old_s, long s) = (1, 0);
    (long old_t, long t) = (0, 1);

    while (r != 0)
    {
        long quotient = old_r / r;
        (old_r, r) = (r, old_r - quotient * r);
        (old_s, s) = (s, old_s - quotient * s);
        (old_t, t) = (t, old_t - quotient * t);
    }

    return (old_s, old_t);
}


