Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class SecureCrypto256
    Public Shared Function Encrypt(ByVal plainText As String, ByVal password As String, ByVal blockSizeBits As Integer) As String
        Dim key As Byte() = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password))

        Dim rij As New RijndaelManaged()
        rij.KeySize = 256
        rij.BlockSize = blockSizeBits
        rij.Mode = CipherMode.CBC
        rij.Padding = PaddingMode.PKCS7
        rij.GenerateIV()

        Dim encryptor As ICryptoTransform = rij.CreateEncryptor(key, rij.IV)

        Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(plainText)
        Dim result As Byte()

        Using ms As New MemoryStream()
            ms.Write(rij.IV, 0, rij.IV.Length) ' Prepend IV
            Using cs As New CryptoStream(ms, encryptor, CryptoStreamMode.Write)
                cs.Write(inputBytes, 0, inputBytes.Length)
                cs.FlushFinalBlock()
            End Using
            result = ms.ToArray()
        End Using

        rij.Clear()
        Return Convert.ToBase64String(result)
    End Function

    Public Shared Function Decrypt(ByVal cipherTextBase64 As String, ByVal password As String, ByVal blockSizeBits As Integer) As String
        Dim fullCipher As Byte() = Convert.FromBase64String(cipherTextBase64)
        Dim key As Byte() = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password))

        Dim rij As New RijndaelManaged()
        rij.KeySize = 256
        rij.BlockSize = blockSizeBits
        rij.Mode = CipherMode.CBC
        rij.Padding = PaddingMode.PKCS7

        Dim ivSize As Integer = blockSizeBits \ 8
        Dim iv(ivSize - 1) As Byte
        Buffer.BlockCopy(fullCipher, 0, iv, 0, ivSize)

        Dim cipherSize As Integer = fullCipher.Length - ivSize
        Dim cipherBytes(cipherSize - 1) As Byte
        Buffer.BlockCopy(fullCipher, ivSize, cipherBytes, 0, cipherSize)

        Dim decryptor As ICryptoTransform = rij.CreateDecryptor(key, iv)
        Dim decrypted As String

        Using ms As New MemoryStream(cipherBytes)
            Using cs As New CryptoStream(ms, decryptor, CryptoStreamMode.Read)
                Using reader As New StreamReader(cs, Encoding.UTF8)
                    decrypted = reader.ReadToEnd()
                End Using
            End Using
        End Using

        rij.Clear()
        Return decrypted
    End Function

    Public Shared Function Verify(ByVal pInpData As String, ByVal pCiper As String, ByVal pKey As String) As Boolean
        Dim deciper As String = Decrypt(pCiper, pKey, 256)
        Return pInpData = deciper
    End Function

    Public Shared Sub Test()
        Dim secret As String = "Sensitive data"
        Dim password As String = "MyStrongPassword"

        Dim encrypted As String = Encrypt(secret, password, 256)
        Console.WriteLine("Encrypted: " & encrypted)

        Dim decrypted As String = Decrypt(encrypted, password, 256)
        Console.WriteLine("Decrypted: " & decrypted)
    End Sub

    Public Shared Function Shuffle(ByVal input As String) As String
        Dim characters As Char() = input.ToCharArray()
        Dim pattern As Integer() = {2, 4, 6, 8, 0, 1, 3, 5, 7, 9} ' Example predefined swap pattern

        For i As Integer = 0 To characters.Length - 1
            Dim target As Integer = i Mod pattern.Length ' Ensures index stays within bounds
            target = pattern(target)
            Dim temp As Char = characters(i)
            Dim targChar As Char = characters(target)
            characters(i) = targChar
            characters(target) = temp
        Next

        Return New String(characters)
    End Function

    Public Shared Function Reshuffle(ByVal shuffled As String) As String
        Dim characters As Char() = shuffled.ToCharArray()
        Dim pattern As Integer() = {2, 4, 6, 8, 0, 1, 3, 5, 7, 9} ' Same pattern, reversing swaps
        For i As Integer = characters.Length - 1 To 0 Step -1
            Dim target As Integer = i Mod pattern.Length ' Ensures index stays within bounds
            target = pattern(target)
            Dim temp As Char = characters(i)
            characters(i) = characters(target)
            characters(target) = temp
        Next

        Return New String(characters)
    End Function

End Class
