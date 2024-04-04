# sharpidious
Static code &amp; rule enforcement analyser for C#

Create a file called `sharpidious.json` in your root folder with your configuration

```
{
    "rules": {
        "class.maxNameLength": 40,
        "class.maxLineCount": 300,
        "class.maxLineLength": 120,
        "method.maxLineCount": 30
    },

    "ignoredClassNames": [
        "Class1",
        "Class2",
    ],

    "rules.override": {
        "Class3": {
            "class.maxNameLength": 123,
            "class.maxLineCount": 456,
            "class.maxLineLength": 1,
            "method.maxLineCount": 789
        }
    }
}
```