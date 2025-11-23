# To learn more about how to use Nix to configure your environment
# see: https://developers.google.com/idx/guides/customize-idx-env
{ pkgs, ... }: {
  # Which nixpkgs channel to use.
  channel = "unstable";
  # Use https://search.nixos.org/packages to find packages
  packages = [
    pkgs.dotnet-sdk_10
    pkgs.nodejs_20
    pkgs.curl
    pkgs.icu
  ];
  # Sets environment variables in the workspace
  env = {
    DOTNET_ROOT = "$HOME/.dotnet";
    PATH = "$HOME/.dotnet:$PATH";
  };
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
    workspace = {
      # Run when the workspace starts
      onStart = {
        install-dotnet-10-stable = ''
          # Install .NET 10 stable if preview is detected
          if dotnet --version 2>&1 | grep -q "preview"; then
            echo "🔧 Installing .NET 10 stable to replace preview version..."
            mkdir -p $HOME/.dotnet
            curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0 --install-dir $HOME/.dotnet --version latest
            export PATH="$HOME/.dotnet:$PATH"
            export DOTNET_ROOT="$HOME/.dotnet"
            echo "✅ .NET 10 stable installed successfully!"
            dotnet --version
          else
            echo "✅ .NET 10 stable already installed"
          fi
        '';
      };
    };
  };
}