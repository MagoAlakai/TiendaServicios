## Create Certificate Request (*.csr) and Encrypted Private Key (*.key) // note: we could create our root before hand

```bash
openssl req -new -out tienda-svc.csr -keyout tienda-svc.key -subj '/CN=tienda-svc' -extensions EXT -config <( printf "[dn]\nCN=tienda-svc\n[req]\ndistinguished_name = dn\n[EXT]\nsubjectAltName=DNS:tienda-svc\nkeyUsage=digitalSignature\nextendedKeyUsage=serverAuth")
```

### PEM Pass Phrase: tienda-svc


## Use the *.key file to create the mathmatically linked Unencrypted RSA Private Key (*.pem)

```bash
openssl rsa -in tienda-svc.key -out tienda-svc.pem
```

## Use the Certificate Request (*.csr) and the Unencrypted RSA Private Key (*.pem) to produce our Public Certificate (*.crt)

```bash
openssl x509 -in tienda-svc.csr -out tienda-svc.crt -req -signkey tienda-svc.pem -days 5475
```


## To generate a pfx file use the following command, password required:

```bash
openssl pkcs12 -export -out tienda-svc.pfx -inkey tienda-svc.key -in tienda-svc.crt
```

## Check contents of the *.pfx file

```bash
openssl pkcs12 -info -in tienda-svc.pfx
```


## Get thumbprint for the *.crt file, it should match the thumbprint of the *.pfx file // SHA1 Fingerprint

```bash
openssl x509 -noout -fingerprint -sha1 -inform pem -in tienda-svc.crt
```


## Get the thumbprint of the *.pfx file, it should match the thumbprint of the *.crt file / SHA1 Fingerprint

```bash
openssl pkcs12 -in tienda-svc.pfx -nodes | openssl x509 -noout -fingerprint
```

OR

```bash
openssl pkcs12 -in tienda-svc.pfx -nodes -passin pass:%1 | openssl x509 -noout -fingerprint
```


## Sample appsettings-json entry for Kestrel HTTP2

```json
  "Kestrel": {
    "EndpointDefaults": {
      "Protocols": "Http2",
      "SslProtocols": [ "Tls12", "Tls13" ],
      "ClientCertificateMode": "AllowCertificate"
    },
    "Endpoints": {
      "Https": {
        "Url": "https://tienda-svc:53443",
        "Port": 53443,
        "Certificate": {
          "Path": "Infrastructure/tienda-svc.pfx",
          "Password": "tienda-svc",
          "AllowInvalid": false
        }
      }
    }
  }
```


## Export the *.pfx into a base64 encoded content, to use in as part of the CI/CD variable set

```bash
openssl base64 -in tienda-svc.pfx -out tienda-svc.pfx.b64
```


## To convert base64 encoded *.pxf.b64 (text file) back into *.pfx (binary file)

```bash
openssl base64 -d -in tienda-svc.pfx.b64 -out tienda-svc.pfx
```

## More checks (Optional)

### Export the private key file from the pfx file
```bash
openssl pkcs12 -in tienda-svc.pfx -nocerts -out key.pem
```

### Export the certificate file from the pfx file (cert.pem == tienda-svc.crt)
```bash
openssl pkcs12 -in tienda-svc.pfx -clcerts -nokeys -out cert.pem
```

### Remove the passphrase from the private key (server.pem == tienda-svc.pem)
```bash
openssl rsa -in key.pem -out server.pem
```

## The file extensions represent different things in the SSL configurations

- *.csr
-- This is the Certificate Request, and in this example above, where we create both the Certificate Request (*.csr) and the Encrypted Private Key (*.key) at the same time.

- *.key
-- This is the encrypted private key, don't share this. It is used later to produce the *.pfx file.

- *.pem
-- Unencrypted/Plain RSA Private key, don't share this one either. The *.pem is used to sign the *.crt file

- *.crt
-- This is the Public Certificate used by clients wishing to connect to a service. It can be used in a PSK setup. It is used later to produce the *.pfx file.

- *.pfx
-- Combined public and private key data, don't share this one either. This is used by the service to distribute the public key. The private key in the *.pfx file is used to decrypt the incoming encrypted data, which has been encrypted by the distributed public key.