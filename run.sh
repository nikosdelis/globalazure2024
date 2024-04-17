sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-8.0

export IOTHUB_DEVICE_CONNECTION_STRING="<CONNECTIONSTRING>"
dotnet run
