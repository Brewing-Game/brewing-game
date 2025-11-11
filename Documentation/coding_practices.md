# Coding Practices

## camelCase vs PascalCase

#### Use **camelCase** for:

- parameters
- arguments
- methodVariables
- functionVariables
- _privateVariables

#### use **PascalCase** for:

- ClassNames
- Methods
- Functions
- PublicMemberVariables
- (I)Interfaces
- Enums

## Style Guidelines

- Use tabs for indentation (no spaces).
- Align code consistently for readability.
- Use the "Allman" style for braces: open and closing braces on their own new line. Braces should line up with the current indentation level.
- Use single-line comments when possible.
- Place comments on the line above the code you’re commenting on.
- Begin comment text with uppercase and end with a period.
- Add a space between the comment operator and the text, e.g., `// This is a good example.`, `//This isn't.`.
- Each class and method should have a comment explaining what it does unless its name is self explanatory.
  
  ```
  // example comment
  example class
  ```
  
- Attempt to write self documenting code and code that limits the need for comments.

## Things to Include

- Add **metric** units within variable names, e.g., `delaySeconds`, `gapMeters`, `emissionGrams`, etc.
- Avoid public fields when possible and serialize fields where required, e.g., `[SerializeField] float example = 0f`.
- Boolean variables should start with "is" or "has," e.g., `bool isOnline`, `bool hasHappened`.
- Use positive boolean versions instead of negative, e.g., use `bool isOnline` vs. `bool isNotOnline`.

## Things to Avoid

- Abbreviated names.
- Single-letter names.
- Adding types in names.
