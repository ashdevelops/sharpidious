### What is sharpidious?

Sharpidious is a small dotnet tool that acts as a static C# code analyser built to enforce certain code rules in your projects.

### How do I get started?

First, create a file called `sharpidious.json` in your root folder.

Here is an example configuration

```
{
    "rules": {
        "class.maxNameLength": 40,
        "class.maxLineCount": 300,
        "class.maxLineLength": 120,
        "method.maxLineCount": 30,
        "check.multipleEmptyLines": true
    }
}
```

Then install the tool globally like so:
`dotnet tool install sharpidious --global`

### Running on a specific project
Run `sharpidious /root/path/to/project`

### Running on current directory
Just run `sharpidious`

### Ignoring certain files
A time might come where you want to exclude a specific file from being checked.

Here is the config for that

```
{
    "rules": {
        "class.maxNameLength": 40,
        "class.maxLineCount": 300,
        "class.maxLineLength": 120,
        "method.maxLineCount": 30,
        "check.multipleEmptyLines": true
    },

    "ignoredClassNames": [
        "SomeClass",
        "SomeOtherClass",
        "AnotherOne"
    ]
}
```

### Overriding rules for specific files

Here is the config for that

```
{
    "rules": {
        "class.maxNameLength": 50,
        "class.maxLineCount": 280,
        "class.maxLineLength": 160,
        "method.maxLineCount": 90,
        "check.multipleEmptyLines": true
    },

    "ignoredClassNames": [
    ],

    "rules.override": {
        "SpecialClass": {
            "class.maxNameLength": 50,
            "class.maxLineCount": 280,
            "class.maxLineLength": 160,
            "method.maxLineCount": 90,
            "check.multipleEmptyLines": false
        }
    }
}
```