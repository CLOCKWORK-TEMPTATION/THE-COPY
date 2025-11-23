#!/bin/sh
#
# Copyright (c) 2021-2024 Doppler, Inc.
#
# This script is licensed under the MIT license.
#
# The MIT License
#
# Copyright (c) 2021-2024 Doppler, Inc.
#
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.

# Shell-agnostic `which` command
_which() {
  command -v "$1" 2>/dev/null
}

_doppler_install_() {
  # Fails on Macs when /usr/local/bin is not in the path, so we do it manually
  if [ -d "/usr/local/bin" ] && [ -w "/usr/local/bin" ]; then
    # The bindir can be overridden with the BIN_DIR environment variable
    BIN_DIR="${BIN_DIR:-"/usr/local/bin"}"
  elif [ -d "/opt/homebrew/bin" ] && [ -w "/opt/homebrew/bin" ]; then
    BIN_DIR="/opt/homebrew/bin"
  else
    # Default to first writable directory in PATH
    for dir in $(echo "$PATH" | tr ":" "\n"); do
      if [ -w "$dir" ]; then
        BIN_DIR="$dir"
        break
      fi
    done
  fi

  if [ -z "$BIN_DIR" ]; then
    echo "Doppler CLI installer: no writable directory found in PATH. You may need to run this script with sudo or check your PATH."
    exit 1
  fi

  # Determine architecture
  ARCH=""
  case "$(uname -m)" in
  x86_64 | amd64)
    ARCH="amd64"
    ;;
  armv*) # Catch armv6l, armv7l
    ARCH="armv6l"
    ;;
  aarch64 | arm64)
    ARCH="arm64"
    ;;
  *)
    echo "Doppler CLI installer: unsupported architecture: $(uname -m)"
    exit 1
    ;;
  esac

  # Determine OS
  OS=""
  case "$(uname -s)" in
  Linux)
    OS="linux"
    ;;
  Darwin)
    OS="darwin"
    ;;
  *)
    echo "Doppler CLI installer: unsupported OS: $(uname -s)"
    exit 1
    ;;
  esac

  # Construct download URL
  DOPPLER_VERSION="${DOPPLER_VERSION:-"latest"}"
  # The CLI version can be overridden with the DOPPLER_VERSION environment variable
  if [ "$DOPPLER_VERSION" != "latest" ]; then
    DOPPLER_VERSION="tags/$DOPPLER_VERSION"
  fi

  DOWNLOAD_URL="https://api.github.com/repos/DopplerHQ/cli/releases/${DOPPLER_VERSION}"

  # Determine download tool
  DOWNLOAD_TOOL=""
  if _which curl >/dev/null; then
    DOWNLOAD_TOOL="curl"
  elif _which wget >/dev/null; then
    DOWNLOAD_TOOL="wget"
  else
    echo "Doppler CLI installer: missing 'curl' or 'wget'"
    exit 1
  fi

  # Download and extract
  echo "Doppler CLI installer: installing to $BIN_DIR"

  if [ "$DOWNLOAD_TOOL" = "curl" ]; then
    DOWNLOAD_CMD="curl -sL"
  else
    DOWNLOAD_CMD="wget -qO-"
  fi

  DOWNLOAD_URL=$($DOWNLOAD_CMD $DOWNLOAD_URL | grep "browser_download_url.*${OS}_${ARCH}" | cut -d \" -f 4 | head -n 1)

  if [ -z "$DOWNLOAD_URL" ]; then
    echo "Doppler CLI installer: could not determine download URL"
    exit 1
  fi

  $DOWNLOAD_CMD "$DOWNLOAD_URL" | tar -xz -C "$BIN_DIR" doppler

  if [ $? != 0 ]; then
      echo "Doppler CLI installer: failed to download and extract"
      exit 1
  fi

  echo "Doppler CLI installer: doppler installed successfully to $BIN_DIR/doppler"
  echo "Doppler CLI installer: run 'doppler login' to get started"
}

_doppler_install_