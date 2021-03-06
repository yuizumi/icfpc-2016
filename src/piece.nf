確認関数群は　|T:確認関数群,geom|の　別名
座標型は　|T:座標型,geom|の　別名
線分型は　|T:線分型,geom|の　別名
直線型は　|T:直線型,geom|の　別名
頂点型は　|T:頂点型,geom|の　別名
辺型は　|T:辺型,geom|の　別名
幾何関数群は　|T:幾何関数群,geom|の　別名


折り紙片型は　クラス　　※　凸多角形
　　幾何関数群を　使用する
　　確認関数群を　使用する

　　頂点は　頂点型の　リストの　フィールド

　　※　＜頂点列＞から　折り紙片型の　インスタンスを作成　→　＜折り紙片＞
　　インスタンスを作成は　クラスメソッド
　　　　引数は　頂点型の　リスト
　　　　戻り値は　折り紙片型

　　　　折り紙片型の　オブジェクトを作成して　新折り紙片と　名付ける
　　　　（引数を）　新折り紙片の　頂点に　入れて　新折り紙片を　返す

　　※　＜折り紙片＞の　頂点数　→　＜数＞
　　頂点数は　メソッド
　　　　戻り値は　整数型
　　　　頂点の　要素数

　　※　＜Ｎ＞番目の　＜折り紙片＞の　辺　→　＜辺＞
　　辺は　メソッド
　　　　引数は　整数型
　　　　戻り値は　辺型

　　　　（引数を）　始点番号と　名付ける
　　　　始点番号に　１を　足して　頂点数で　割った余りを　終点番号と　名付ける
　　　　頂点［始点番号］と　頂点［終点番号］で　辺を作成

　　※　折るの戻り値の型
　　折った結果型は　構造体
　　　　折った側は　折り紙片型の　フィールド
　　　　残った側は　折り紙片型の　フィールド

　　※　＜座標＞から　＜座標＞に　＜折り紙片＞を　折る　→　＜結果＞
　　折るは　メソッド
　　　　引数は　座標型、座標型
　　　　戻り値は　折った結果型

　　　　（第二引数を）　移動後の点と　名付ける
　　　　（第一引数を）　移動前の点と　名付ける

　　　　頂点型の　リスト型の　オブジェクトを作成して　折った側の点と　名付ける
　　　　頂点型の　リスト型の　オブジェクトを作成して　残った側の点と　名付ける

　　　　移動前の点と　移動後の点の　垂直二等分線を　折り線と　名付ける
　　　　移動前の点と　折り線との　位置関係を　折る側と　名付ける
　　　　移動後の点と　折り線との　位置関係を　残る側と　名付ける

　　　　折る側と　残る側の　符号反転が　等しいことを　確認する

　　　　頂点数から　１を　引いて　最大番号と　名付ける
　　　　０から　最大番号までの　範囲の各数値を　番号という　新しい変数に入れて
　　　　　　頂点［番号］の　今の座標と　折り線との　位置関係を　頂点位置と　名付ける
　　　　　　頂点位置が　折る側に　等しい　ならば
　　　　　　　　折り線で　頂点［番号］を　折り返して　折った側の点に　追加する
　　　　　　頂点位置が　残る側に　等しい　ならば
　　　　　　　　頂点［番号］を　残った側の点に　追加する
　　　　　　折り線が　番号の　辺の　線分と　交わる？　ならば
　　　　　　　　折り線と　番号の　辺の　線分をのばした　直線との　交点の
　　　　　　　　　　番号の　辺における　頂点を求める
　　　　　　　　複写して　折った側の点に　追加して　残った側の点にも　追加する

　　　　折った結果型の　オブジェクトを作成して
　　　　　　結果という　新しい変数に入れる
　　　　折った側の点の　要素数が　３　以上　ならば
　　　　　　折側の点から　折り紙片型の　インスタンスを作成　結果の　折側に　入れる
　　　　残った側の点の　要素数が　３　以上　ならば
　　　　　　残側の点から　折り紙片型の　インスタンスを作成　結果の　残側に　入れる

　　　　結果を　返す
