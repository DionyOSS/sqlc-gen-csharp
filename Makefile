SHELL 		:= /bin/bash
PWD 		:= $(shell pwd)

RUNTIME_DIR := SqlcGenCsharp/bin/Release/.net8.0/osx-arm64 # TODO fix
PATH  		:= ${PATH}:${PWD}/${RUNTIME_DIR}

protobuf-generate:
	buf generate --template buf.gen.yaml buf.build/sqlc/sqlc --path plugin/

dotnet-build:
	dotnet build

dotnet-publish: protobuf-generate dotnet-build
	dotnet publish SqlcGenCsharp -c release --output dist/

dotnet-publish-with-wasm: dotnet-publish
	cp ${RUNTIME_DIR}/SqlcGenCsharp.wasm dist/plugin.wasm

sqlc-generate-from-exe: dotnet-publish
	sqlc -f sqlc.process.yaml generate

sqlc-generate-from-wasm: dotnet-publish-with-wasm
	sqlc -f sqlc.wasm.yaml generate

run-tests:
	./run_tests.sh

test-process-plugin: sqlc-generate-from-exe run-tests

test-wasm-plugin: sqlc-generate-from-wasm run-tests