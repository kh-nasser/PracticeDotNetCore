using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Security
{
    public class SecureFile
    {
    //    public void SetFileAccess(string path)
    //    {
    //        //FileInfo fInfo = new FileInfo(fileName);
    //        //FileSecurity security = fInfo.GetAccessControl();
    //        //var owner = security.GetOwner(typeof(System.Security.Principal.NTAccount));
    //        //var group = security.GetGroup(typeof(System.Security.Principal.NTAccount));
    //        //var others = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.BuiltinUsersSid, null)
    //        //                 .Translate(typeof(System.Security.Principal.NTAccount));
    ////        security.ModifyAccessRule(AccessControlModification.Add,
    ////new FileSystemAccessRule(owner, FileSystemRights.Modify, AccessControlType.Allow),
    ////out bool modified);
            

    //        const string AccountIdentity = "identityOfUser";

    //        //var fileSecurity = new FileSecurity();
    //        var ds = new DirectorySecurity();
    //        var readRule = new FileSystemAccessRule(AccountIdentity, FileSystemRights.ReadData, AccessControlType.Allow);
    //        var writeRule = new FileSystemAccessRule(AccountIdentity, FileSystemRights.WriteData, AccessControlType.Allow);
    //        var noExecRule = new FileSystemAccessRule(AccountIdentity, FileSystemRights.ExecuteFile, AccessControlType.Deny);

    //        ds.SetAccessRuleProtection(true, false); // disable inheritance and clear any inherited permissions
    //        ds.AddAccessRule(readRule);
    //        ds.AddAccessRule(writeRule);
    //        ds.AddAccessRule(noExecRule);

    //        FileSystemAclExtensions.SetAccessControl(new DirectoryInfo(path), ds);
    //        //File.SetAccessControl(path, fileSecurity);
    //    }
    }
}
