座標型は　構造体
　　Ｘ座標は　分数型の　フィールド
　　Ｙ座標は　分数型の　フィールド

　　変換は　クラスメソッド
　　　　引数は　文字列型
　　　　戻り値は　座標型

　　　　（引数を）　座標文字列という　新変数に入れる
　　　　「,」を　文字型の配列に変換して　座標文字列を　分割して
　　　　　　Ｓという　新変数に入れる
　　　　Ｐは　座標型の　変数
　　　　Ｓ［０］を　分数型に　変換して　Ｐの　Ｘ座標に　入れる
　　　　Ｓ［１］を　分数型に　変換して　Ｐの　Ｙ座標に　入れる
　　　　Ｐを　返す
