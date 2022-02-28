using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public interface IUnityOfWork: IDisposable
{
	void Commit();
	int GetLastIdInsert();
}
