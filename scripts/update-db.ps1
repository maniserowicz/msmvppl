param([string]$username, [string]$password)

$sourceuri = "ftp://ftp.msmvp.pl/msmvpdata.pl/wwwroot/App_Data/msmvp_data.sdf"
$targeturi = "ftp://ftp.msmvp.pl/msmvp.pl/wwwroot/App_Data/msmvp_data.sdf"
$targetpath = ".\msmvp_data.sdf"

function prepareRequest{
  param([string]$uri)
  $ftprequest = [System.Net.FtpWebRequest]::create($uri)
  $ftprequest.Credentials = New-Object System.Net.NetworkCredential($username,$password)
  $ftprequest.EnableSsl = $true
  $ftprequest.UseBinary = $true
  $ftprequest.KeepAlive = $false
  [System.Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
  return $ftprequest
}

write-output "Downloading db..."

$ftprequest = prepareRequest($sourceuri);
$ftprequest.Method = [System.Net.WebRequestMethods+Ftp]::DownloadFile
$ftpresponse = $ftprequest.GetResponse()
$responsestream = $ftpresponse.GetResponseStream()
$targetfile = New-Object IO.FileStream ($targetpath,[IO.FileMode]::Create)
[byte[]]$readbuffer = New-Object byte[] 1024
do{
    $readlength = $responsestream.Read($readbuffer,0,1024)
    $targetfile.Write($readbuffer,0,$readlength)
}
while ($readlength -ne 0)
$targetfile.close()

write-output "Uploading db..."

$bytes = [System.IO.File]::ReadAllBytes($targetpath)

$ftprequest = prepareRequest($targeturi);
$ftprequest.Method = [System.Net.WebRequestMethods+Ftp]::UploadFile
$ftprequest.ContentLength = $bytes.Length;
$requestStream = $ftprequest.GetRequestStream();
$requestStream.Write($bytes, 0, $bytes.Length);
$requestStream.Close();
$ftpresponse = $ftprequest.GetResponse()

write-output "Copying db..."

copy $targetpath .\..\src\msmvp_pl\App_Data\msmvp_data.sdf
copy $targetpath .\..\src\DataEntryApp\App_Data\msmvp_data.sdf
rm $targetpath

write-output "Done!"