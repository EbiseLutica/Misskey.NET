# Misskey .NET

日本語・[English](README-en.md)

難しいことを考えず、手軽に扱える。新しい Misskey API ライブラリ。

## 認証せずに使う

```cs
var io = new Misskey("misskey.io");

try
{
    var user = await io.ApiAsync<Dictionary<string, object>>("users/show", new
    {
        userId = "7rkr2cvs0v",
    });
    Console.WriteLine(user["username"]); // Admin
}
catch (MisskeyApiException e)
{
    // エラーオブジェクトがサーバーから返ってきた場合
    Console.WriteLine("ユーザー情報の取得に失敗しました。");
}
catch (HttpException e)
{
    // サーバーにて問題が発生している場合
    Console.WriteLine(e.Message);
}
```

## MiAuth 認可 (for Misskey v12)

```cs
var miAuth = new MiAuth("misskey.io", "MissDeck", "https://missdeck.example.com/icon.png", null, Permission.All);
if (!miAuth.TryOpenBrowser())
{
    Console.WriteLine("次のURLをお使いのウェブブラウザーで開き、認証を完了させてください。");
    Console.WriteLine(miAuth.Url);
}
Console.WriteLine("認可が完了したら、ENTER キーを押してください。");
Console.ReadLine();

try
{
    Misskey io = await miAuth.CheckAsync();
    var i = io.ApiAsync<Dictionary<string, object>("i");
    Console.WriteLine(user["username"]); // 認証したユーザーのユーザー名
}
catch (MisskeyApiException e)
{
    // エラーオブジェクトがサーバーから返ってきた場合
    Console.WriteLine("認可に失敗しました。");
}
catch (HttpException e)
{
    // サーバーにて問題が発生している場合
    Console.WriteLine(e.Message);
}
```

## レガシー認可 (for Misskey v10, v11)

サポートしていません。（コントリビューションを歓迎します）

## 認証情報のエクスポート

アプリを使用するたびにユーザーに認証させるのはあまり良くありません。Misskey クラスは、認証情報のインポート/エクスポートをサポートします。

```cs
// 単純なINI形式の文字列にシリアライズされる
// トークンなど機密情報が含まれるので、暗号化して保存すると良い
string serialized = misskey.Export();

// シリアライズした文字列から Misskey クラスのインスタンスを生成
Misskey m = Misskey.Import(serialized);
```

## ストリーミング API
Coming soon

<!-- 

ストリーミング API を使用することで、Misskey インスタンスからリアルタイムに情報を取得できます。本ライブラリでは、ストリーミングを経由した Misskey API 呼び出しはサポートしません。

ストリーミング API を有効化する場合は次のように書きます。

```cs
// Misskey misskey;
MisskeyStreaming stream = await misskey.OpenStreamAsync();
``` -->

