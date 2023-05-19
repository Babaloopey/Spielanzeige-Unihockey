Unihockey Spielanzeige

Created by Florin Rüedi


How to generate testreport:
dotnet test --collect:"XPlat Code Coverage"

How to format to HTML:
reportgenerator -reports:SpielanzeigeTestNUnit\TestResults\*\coverage.cobertura.xml -targetdir:SpielanzeigeTestNUnit\TestResults -reporttypes:Html
