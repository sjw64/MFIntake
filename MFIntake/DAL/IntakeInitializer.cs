using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MFIntake.Models;

namespace MFIntake.DAL
{
    public class IntakeInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<IntakeContext>
    {
        protected override void Seed(IntakeContext context)
        {
            var evidence = new List<Intake>
            {
            new Intake{CaseNumber="ABCD-123",DeviceModel="SGH-A767",FullName="Sandi Wentworth",Custodian="Sherlock Holmes",StorageLocation="EL-B1",WarrantNumber="53-32092d2",IntakeStatus="Received",RequestedByDate=DateTime.Parse("2017-03-31"), ReceivedDate=DateTime.Parse("2017-01-11"), IntakeNote="Intake Note"},
            new Intake{CaseNumber="BCDE-234",DeviceModel="iPhone 6s",FullName="Kamal Singh",Custodian="P. Watson",StorageLocation="EL-B2",WarrantNumber="54-32192d2",IntakeStatus="In Progress",RequestedByDate=DateTime.Parse("2017-03-31"), ReceivedDate=DateTime.Parse("2017-01-12"), IntakeNote="Intake Note"},
            new Intake{CaseNumber="CDEF-345",DeviceModel="Droid Maxx",FullName="Sandi Wentworth",Custodian="P. Watson",StorageLocation="EL-B3",WarrantNumber="55-32252d2",IntakeStatus="Exams Completed",RequestedByDate=DateTime.Parse("2017-04-20"), ReceivedDate=DateTime.Parse("2017-01-16"), IntakeNote="Intake Note"},
            new Intake{CaseNumber="DEFG-456",DeviceModel="M360",FullName="Kamal Singh",Custodian="Sherlock Holmes",StorageLocation="EL-B4",WarrantNumber="56-32392d2",IntakeStatus="Received",RequestedByDate=DateTime.Parse("2017-03-15"), ReceivedDate=DateTime.Parse("2017-01-20"), IntakeNote="Intake Note"}
            };

            evidence.ForEach(s => context.Intakes.Add(s));
            context.SaveChanges();

            var exams = new List<Exam>
            {
            new Exam{IntakeID=2,ExamType="BitPim",ExamStatus="Exam Completed",Analyst="John Bair",ExamDate=DateTime.Parse("2017-01-16"),AddlEquipNeeded=false,ExamFileLocation="c:\\BCDE-234\\BitPim\\", ExamNote="Exam Note"},
            new Exam{IntakeID=2,ExamType="UFED PA",ExamStatus="Exam Completed",Analyst="Josh Nipert",ExamDate=DateTime.Parse("2017-01-22"),AddlEquipNeeded=true,ExamFileLocation="c:\\BCDE-234\\BitPim\\", ExamNote="Exam Note"},
            new Exam{IntakeID=2,ExamType="JTAG",ExamStatus="Exam Completed",Analyst="John Bair",ExamDate=DateTime.Parse("2017-01-25"),AddlEquipNeeded=true,ExamFileLocation="c:\\BCDE-234\\BitPim\\", ExamNote="Exam Note"},
            new Exam{IntakeID=3,ExamType="SecureView",ExamStatus="Exam Completed",Analyst="Prabhjot Singh",ExamDate=DateTime.Parse("2017-01-18"),AddlEquipNeeded=false,ExamFileLocation="c:\\BCDE-234\\BitPim\\", ExamNote="Exam Note"},
            new Exam{IntakeID=3,ExamType="UFED PA",ExamStatus="Exam In Progress",Analyst="Josh Nipert",ExamDate=DateTime.Parse("2017-01-22"),AddlEquipNeeded=false,ExamFileLocation="c:\\BCDE-234\\BitPim\\", ExamNote="Exam Note"}
            };
            exams.ForEach(s => context.Exams.Add(s));
            context.SaveChanges();

            var custodians = new List<Custodian>
            {
            new Custodian {CustodianFlag=true,LastName="Holmes", FirstName="Sherlock",Email="sholmes@abc.com",phoneNumber="253-555-1212",AgencyName="ABC Detectives"},
            new Custodian {CustodianFlag=true,LastName="Watson", FirstName="P.",Email="pwatson@abc.com",phoneNumber="253-555-1213",AgencyName="ABC Detectives"}
            };
            custodians.ForEach(s => context.Custodians.Add(s));
            context.SaveChanges();

            var agents = new List<Agent>
            {
            new Agent {agentFlag=true,LastName="Wentworth", FirstName="Sandi",Email="swentworth@wsp.com",phoneNumber="253-555-1214",AgencyName="WA State Patrol"},
            new Agent {agentFlag=true,LastName="Singh", FirstName="Kamal",Email="ksingh@kent.org",phoneNumber="253-555-1215",AgencyName="City of Kent"}
            };
            agents.ForEach(s => context.Agents.Add(s));
            context.SaveChanges();

            var analysts = new List<Person>
            {
            new Person {discriminator="Analyst",LastName="Bair", FirstName="John",Email="jbair007@uw.edu",phoneNumber="253-555-1216",AgencyName="UW Tacoma"},
            new Person {discriminator="Analyst",LastName="Nipert", FirstName="Josh",Email="jnipert@uw.edu",phoneNumber="253-555-1217",AgencyName="UW Tacoma"},
            new Person {discriminator="Analyst",LastName="Singh", FirstName="Prabhjot",Email="psingh@uw.edu",phoneNumber="253-555-1218",AgencyName="UW Tacoma"}
            };
            analysts.ForEach(s => context.Persons.Add(s));
            context.SaveChanges();

            var status = new List<Status>
            {
            new Status{StatusType="Intake",StatusName="Received",Description="Newly Received Evidence"},
            new Status{StatusType="Intake",StatusName="In Progress",Description="Exams In Progress"},
            new Status{StatusType="Intake",StatusName="Exams Completed",Description="Exams completed, compliling reports"},
            new Status{StatusType="Intake",StatusName="Returned",Description="Returned Evidence and reports"},
            new Status{StatusType="Exam",StatusName="Exam In Progress",Description="Evidence is in process of being examined"},
            new Status{StatusType="Exam",StatusName="Equip Ordered",Description="Exam on Hold, Equipment Ordered"},
            new Status{StatusType="Exam",StatusName="Exam Completed",Description="Exam Completed"},
            new Status{StatusType="Order",StatusName="Order Placed",Description="Equipment Order Placed"},
            new Status{StatusType="Order",StatusName="Order Received",Description="Equipment Order Received"},
            new Status{StatusType="Order",StatusName="Bill Grant",Description="Billed to Grant"},
            new Status{StatusType="Order",StatusName="Bill Agency",Description="Billed to Agency"},
            new Status{StatusType="Order",StatusName="Pmt Received",Description="Payment Received from Agency"},
            };
            status.ForEach(s => context.Statuses.Add(s));
            context.SaveChanges();

        }

    }
}