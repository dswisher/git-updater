
.PHONY: install uninstall update pack

pack:
	dotnet pack

install: pack
	dotnet tool install --global --add-source ./nupkg GitUpdater

update: pack
	dotnet tool update --global --add-source ./nupkg GitUpdater

uninstall:
	dotnet tool uninstall --global GitUpdater

