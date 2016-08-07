確認関数群は　|T:確認関数群,geom|の　別名
座標型は　|T:座標型,geom|の　別名
線分型は　|T:線分型,geom|の　別名
直線型は　|T:直線型,geom|の　別名
頂点型は　|T:頂点型,geom|の　別名
辺型は　|T:辺型,geom|の　別名
幾何関数群は　|T:幾何関数群,geom|の　別名

折り紙片型は　|T:折紙片型,piece|の　別名


折り紙型は　クラス
　　幾何関数群を　使用
　　確認関数群を　使用

　　折り紙片は　折り紙片型の　リスト型の　フィールド

　　※　折り紙型の　インスタンスを作成　→　＜初期状態＞
　　インスタンスを作成は　クラスメソッド
　　　　戻り値は　折り紙型

　　　　頂点型の　リスト型の　オブジェクトを作成　頂点列と　名付ける

　　　　０，０の　座標で　頂点を作成　頂点列に　追加する
　　　　１，０の　座標で　頂点を作成　頂点列に　追加する
　　　　１，１の　座標で　頂点を作成　頂点列に　追加する
　　　　０，１の　座標で　頂点を作成　頂点列に　追加する
　　　　
　　　　折り紙型の　オブジェクトを作成して　新しい折り紙と　名付ける

　　　　折り紙片型の　リスト型の　オブジェクトを作成
　　　　　　新しい折り紙の　折り紙片に　入れる
　　　　頂点列から　折り紙片型の　インスタンスを作成
　　　　　　新しい折り紙の　折り紙片に　追加する

　　　　新しい折り紙を　返す

　　※　＜座標＞から　＜座標＞に　＜折り紙＞を　折る　→　・
　　折るは　メソッド
　　　　引数は　座標型、座標型

　　　　（第二引数を）　点Ｑと　名付ける
　　　　（第一引数を）　点Ｐと　名付ける

　　　　折り紙片型の　リスト型の　オブジェクトを作成して
　　　　　　下側と　名付ける
　　　　折り紙片型の　リスト型の　オブジェクトを作成して
　　　　　　上側と　名付ける

　　　　折り紙片の　各要素を
　　　　　　その折り紙片と　名付けて
　　　　　　点Ｐから　点Ｑに　その折り紙片を　折って　結果と　名付ける
　　　　　　結果の　残った側が　ヌルと　異なる　ならば
　　　　　　　　結果の　残った側を　下側に　追加する
　　　　　　結果の　折った側が　ヌルと　異なる　ならば
　　　　　　　　結果の　折った側を　上側に　追加する

　　　　折り紙片を　クリアする
　　　　下側を　折り紙片に　まとめて追加する
　　　　上側を　逆順にする
　　　　上側を　折り紙片に　まとめて追加する

　　※　＜折り紙＞の　状態文字列　→　＜文字列＞
　　状態文字列は　メソッド
　　　　戻り値は　文字列型

　　　　頂点型から　整数型への　辞書型の　オブジェクトを作成して
　　　　　　頂点番号表と　名付ける
　　　　頂点型の　リスト型の　オブジェクトを作成して　頂点列と　名付ける

　　　　折り紙片の　各要素につき
　　　　　　頂点の　各要素を　その頂点と　名付け
　　　　　　　　その頂点が　頂点番号表に　キーとして存在？
　　　　　　　　　　ならば　次に移る
　　　　　　　　頂点列の　要素数を　頂点番号表［その頂点］に　入れる
　　　　　　　　その頂点を　頂点列に　追加する

　　　　頂点列の　要素数から　１を　引いて　最大番号と　名付ける
　　　　０から　最大番号までの　範囲の各数値を　番号と　名付けて
　　　　　　頂点列［番号］を　その頂点と　名付けて
　　　　　　頂点番号表［その頂点］が　番号に　等しいことを　確認する

　　　　文字列出力型の　オブジェクトを作成して　出力と　名付ける

　　　　頂点列の　要素数を　出力に　改行付きで書き出す
　　　　頂点列の　各要素の
　　　　　　元の座標を　文字列化して　出力に　改行付きで書き出す

　　　　折り紙片の　要素数を　出力に　改行付きで書き出す
　　　　折り紙片の　各要素を　その折り紙片と　名付け
　　　　　　その折り紙片の　頂点数を　出力に　書き出す
　　　　　　その折り紙片の　頂点の　各要素を　その頂点と　名付け
　　　　　　　　「 」と　出力に　書き出す
　　　　　　　　頂点番号表［その頂点］を　出力に　書き出す
　　　　　　出力を　改行する

　　　　頂点列の　各要素の
　　　　　　今の座標を　文字列化して　出力に　改行付きで書き出す

　　　　出力を　文字列化する