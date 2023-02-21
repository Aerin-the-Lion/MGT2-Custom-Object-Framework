# MGT2-Custom-Object-Framework
Mod Framework to create custom mod objects for modder.

### 日本語解説 ###
Mad Games Tycoon 2用のModフレームワーク。<br>
新たなオブジェクトを生成するためのModder向けフレームワークModです。<br>
<br>
英語書くの疲れるので、最初に日本語で書いておきます。備忘録です。<br>
<br>
#やっていること
既存のオブジェクトのIDを元にオブジェクトをクローンとして生成し、それらの設定を簡単に調整できるようにするフレームワークです。<br>
現状、機能としてはそれだけなんですが、一からこの作業を行うとすると非常に手間がかかりますし、ドイツ語のコードがちょくちょくあるので自分は必要かなと考えました。<br>
<br>
#使い方
MCOFramework.dllをBepInEx/pluginsとかに入れておいて、Mod作成する時に参照してusingでこのModを読み込んでください。<br>
CustomModObject hoge = new CustomModObject();<br><br>
で、hoge.fugaの要領で設定できるようになります。なおコンストラクタで、引数指定できるのでそっちのほうが楽だと思います。<br>
ですが、こうするとConfigManagerでゲーム内で動的に数値を動かすことができないと分かったので、仕様変更するかも知れません。<br>
