
.PHONY: install uninstall update pack

pack:
	dotnet pack

install: pack
	dotnet tool install --global --prerelease --add-source ./nupkg GitUpdater

update: pack
	dotnet tool update --global --prerelease --add-source ./nupkg GitUpdater

uninstall:
	dotnet tool uninstall --global GitUpdater

