# CertificateEnumerator

![Screenshot 1](https://github.com/AdamWhiteHat/CertificateEnumerator/blob/master/CertManager.png)

| A list of certificates | A closer look at the reason a cert is invalid |
| --- | --- |
| ![Screenshot 2](https://github.com/AdamWhiteHat/CertificateEnumerator/blob/master/CertificateEnumerator.PNG) | ![Screenshot 3](https://github.com/AdamWhiteHat/CertificateEnumerator/blob/master/CertificateEnumerator2.png) |




CertificateEnumerator can list every certificate in your various certificate stores for your local machine and currently logged in user. It can then display that information to you either in a DataGridView or TextBox (as columnarized text), and provides the ability to persist that information to file as text, comma separated values (CSV), excel format or HTML table. 

The Certificate Enumerator also has the ability to 'validate' each certificate against its CRL (certificate revocation list), if it supplied one.

Update: Cert Enumerator now has the ability to extract CRL (Certificate Revocation List) URLs from certificates, download the CRLs and install the revocation lists in your untrusted cert store!

You can read my blog post about it @ http://www.csharpprogramming.tips/2015/08/certificate-enumerator.html
