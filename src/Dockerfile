FROM microsoft/dotnet:2.2-sdk AS builder
COPY . /src
WORKDIR /src
RUN apt-get update && apt-get install -y curl \
  && curl -sL https://deb.nodesource.com/setup_9.x | bash - \
  && apt-get install -y nodejs \
  && curl -L https://www.npmjs.com/install.sh | sh \
  && apt-get install -y libpng-dev \
  && apt-get install -y build-essential
RUN npm install -g node-gyp
RUN dotnet publish -c Release -o /recruitme RecruitMe.Web

FROM microsoft/dotnet:2.2-sdk AS runtime
COPY --from=builder /recruitme .
RUN apt-get update \
  && apt-get install -y libsm6 libxext6 libxrender-dev libgdiplus \
  && apt-get install -y python3-pip python3-dev \
  && cd /usr/local/bin \
  && ln -s /usr/bin/python3 python \
  && pip3 install --upgrade pip \
  && pip install imutils \
  && pip install numpy \
  && pip install opencv-python \
  && pip install scipy
EXPOSE 443
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT Release
ENTRYPOINT ["dotnet", "RecruitMe.Web.dll"]