[english version](ReadMe-en.md)

我跟你讲，之前的那个叫[wcpc](../wcpc)的玩意有bug。啥bug呢？在前两天的实践中，我发现它对行的识别出错了。

另外按行来搞总是很慢，所以就出了这么一个快速版。至于wcpc用分段缓冲来搞，简单想了一下代码有点复杂暂时没心情。

现在这玩意处理速率虽然赶不上C++，但也差不了多少，代码写起来也快，方便。

要搞的文件别太大，1个G就够吃的了。超过2个G的文件没试过，机器太渣也试不起。不过估计不支持2G以上的文件读写。

另外也不支持超长路径，至于Unicode路径，这是C#的特点可以放心用。

用法很简单：

```batch
echo <scp>[-dcp[-fstr]] | qcpc <input> [output]
```
```
Default:
echo 0-1200-? | qcpc
```

毫不犹豫地转换一个文件为Unicode：
```batch
echo 932 | qcpc script.txt
```
转成UTF-8：
```batch
echo 932-65001 | qcpc in.txt out.txt
```
无法转换的字符不要问号，要下划线，或者要个人标记
```batch
echo 932-936-_ | qcpc o1.txt
echo 932-936-hello | qcpc o2.txt
```
