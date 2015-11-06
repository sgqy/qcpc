[english version](ReadMe-en.md)

My project [wcpc](../wcpc) has a bug. It would wrongly identify a line in an irregular file.

So here is a very simple version using .net framework.

Usage:
```batch
echo <scp>[-dcp[-fstr]] | qcpc <input> [output]
```
```
Default:
echo 0-1200-? | qcpc
```

Just convert a file to Unicode：
```batch
echo 932 | qcpc script.txt
```
Convert to UTF-8：
```batch
echo 932-65001 | qcpc in.txt out.txt
```
Default char not using question mark and custom it:
```batch
echo 932-936-_ | qcpc o1.txt
echo 932-936-hello | qcpc o2.txt
```
