podman run --rm -it `
  -v "${PWD}:/work" -w /work `
  -v nuget_cache:/root/.nuget/packages `
  build-env:1.0 `
  bash -lc "dotnet tool restore && dotnet tool run nuke -- All"
