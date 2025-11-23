# To learn more about how to use Nix to configure your environment
# see: https://developers.google.com/idx/guides/customize-idx-env
{ pkgs, ... }: {
  # Which nixpkgs channel to use.
  channel = "unstable"; # Use unstable channel to get latest .NET 10 stable release
  # Use https://search.nixos.org/packages to find packages
  packages = [ pkgs.dotnet-sdk_10 pkgs.nodejs_20 ];
  # Sets environment variables in the workspace
  env = { };
  idx = {
    # Search for the extensions you want on https://open-vsx.org/ and use "publisher.id"
    extensions = [ "muhammad-sammy.csharp" ];
    # Enable previews and customize configuration
    previews = {
      enable = true;
      previews = {
        web = {
          command = [ "dotnet" "watch" "--urls=http://localhost:$PORT" ];
          manager = "web";
        };
      };
    };
  };
}
