// <auto-generated />
namespace MyZoo.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class ChangeNameFromJournalIdToAnimalIdInJournal : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ChangeNameFromJournalIdToAnimalIdInJournal));
        
        string IMigrationMetadata.Id
        {
            get { return "201711102036097_ChangeNameFromJournalIdToAnimalIdInJournal"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
