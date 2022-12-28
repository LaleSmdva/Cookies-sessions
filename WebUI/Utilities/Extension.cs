using Microsoft.AspNetCore.Http;

namespace WebUI.Utilities;

public static class Extension
{
	public static bool CheckFileSize(this IFormFile file, int kByte)
	{
        return file.Length/1024<=kByte;
	}
	public static bool CheckImage(this IFormFile file, string path)
	{
		return file.ContentType.Contains(path);
	}

    public static async  Task<string> CopyFileAsync(this IFormFile file, string wwwroot,params string[] folders)
    {
        var filename = Guid.NewGuid().ToString() + file.FileName;
        var resultpath = wwwroot;
        foreach (var folder in folders)
        {
            resultpath = Path.Combine(resultpath,folder);
        }
        resultpath= Path.Combine(resultpath, filename); 

        using (FileStream stream = new FileStream(resultpath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filename;
    }
}
