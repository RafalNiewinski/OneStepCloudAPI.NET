using OneStepCloudAPI;
using OneStepCloudAPI.Exceptions;
using OneStepCloudAPI.OneStepObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneStepCloudAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //OneStepClient cl = new OneStepClient(OneStepRegion.PL);
            //cl.SignIn("razdwatrzy", "***REMOVED***").Wait();

            //VirtualMachine vm = cl.VirtualMachines.Get(326).Result;

            /*cl.VirtualMachines.Suspend(326);
            Console.WriteLine("SUSPENDED");
            cl.VirtualMachines.SnapshotCreate(326);
            Console.WriteLine("SNAP CREATED");
            cl.VirtualMachines.PowerOn(326);
            Console.WriteLine("STARTED");
            cl.VirtualMachines.SnapshotRevert(326);
            Console.WriteLine("SNAP REVERTED");
            cl.VirtualMachines.PowerOn(326);
            Console.WriteLine("STARTED");
            cl.VirtualMachines.SnapshotDelete(326);
            Console.WriteLine("SNAP DELETED");*/

            //StartOscOperations(cl);

            //OneStepClient uscl = new OneStepClient(OneStepRegion.PL);
            //uscl.SignIn("rafalniewinski", "***REMOVED***").Wait();

            //DELETE ALL
            /*List<VirtualMachineSummary> vms = uscl.VirtualMachines.GetAll().Result;
            List<Task> deltasks = new List<Task>();
            foreach (var vm in vms)
                if(vm.id != 315 && vm.id != 316)
                    deltasks.Add(uscl.VirtualMachines.Delete(vm));
            Task.WaitAll(deltasks.ToArray());*/

            /*List<VirtualMachineDisk> disks = new List<VirtualMachineDisk>
            {
                new VirtualMachineDisk{ id = 0, primary = true, storage_gb = 10, storage_type = VirtualMachineDiskStorageType.optimal },
                new VirtualMachineDisk { id = 0, primary = false, storage_gb = 15, storage_type = VirtualMachineDiskStorageType.optimal }
            };*/

            //VirtualMachine ubuntu = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 2, memory_mb = 2048, product_id = 4, virtual_machine_disks = disks }).Result;
            //VirtualMachine centos = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 2, memory_mb = 2048, product_id = 5, virtual_machine_disks = disks }).Result;
            //VirtualMachine debian = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 2, memory_mb = 2048, product_id = 19, virtual_machine_disks = disks }).Result;
            //VirtualMachine suse = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 2, memory_mb = 2048, product_id = 6, virtual_machine_disks = disks }).Result;
            //VirtualMachine fedora = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 2, memory_mb = 2048, product_id = 22, virtual_machine_disks = disks }).Result;
            //VirtualMachine arch = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 2, memory_mb = 2048, product_id = 23, virtual_machine_disks = disks }).Result;

            //Task[] conftasks = { uscl.VirtualMachines.Configure(ubuntu, new VirtualMachineConfigurationPrototype { virtual_machine_name = "UbuntuKEY", ssh_key = true, private_key_id = 20 }),
            //uscl.VirtualMachines.Configure(centos, new VirtualMachineConfigurationPrototype { virtual_machine_name = "CentosKEY", ssh_key = true, private_key_id = 14 }) };
            //uscl.VirtualMachines.Configure(debian, new VirtualMachineConfigurationPrototype { virtual_machine_name = "DebianKEY", use_private_key = true, private_key_id = 38 });
            //uscl.VirtualMachines.Configure(suse, new VirtualMachineConfigurationPrototype { virtual_machine_name = "SuseKEY", use_private_key = true,  private_key_id = 38 });
            //uscl.VirtualMachines.Configure(fedora, new VirtualMachineConfigurationPrototype { virtual_machine_name = "FedoraKEY", use_private_key = true,  private_key_id = 38 });
            //uscl.VirtualMachines.Configure(arch, new VirtualMachineConfigurationPrototype { virtual_machine_name = "ArchKEY", use_private_key = true,  private_key_id = 38 });

            //Task.WaitAll(conftasks);

            /*seconduser.user_detail.first_name = "Jan";
            seconduser.user_detail.last_name = "Nazwisko";
            seconduser.user_detail.phone_number = "5555";
            seconduser.user_detail.mobile_number = "";
            uscl.Users.UpdateUserDetails(seconduser).Wait();*/

            try
            {
                OneStepClient uscl = new OneStepClient(OneStepRegion.PL);
                uscl.SignIn("rafalniewinski", "***REMOVED***").Wait();

                List<User> users = uscl.Users.GetAll().Result;
                User seconduser = users.Where(x => x.Username == "seconduser").First();

                /*bool emailcheck = uscl.Users.EmailAvailable("dakodaskdosadk").Result;
                bool usercheck = uscl.Users.UsernameAvailable("seconduser").Result;
                uscl.Users.SendInvitation("b@b.bb", "thedude").Wait();*/

                List<Invitation> invites = uscl.Users.GetInvitations().Result;
                foreach (var i in invites)
                    uscl.Users.CancelInvitation(i).Wait();

                //uscl.Users.DeleteUser(users.Where(x => x.id == 57).First()).Wait();

                GroupLimits limits = uscl.SessionSummary().Result.GroupLimits;
                Console.WriteLine(String.Format("Summary vms: {0}/{1}", limits.Vms.Current, limits.Vms.Limit));

                Console.WriteLine("Maintenance: " + uscl.MainenanceMessage().Result);

                PrintNetworks(uscl);

                /*var newnett = uscl.Network.Create();
                PrintNetworks(uscl);
                var newnet = newnett.Result;
                Console.WriteLine("IP address obtained: " + newnet.ip_address);
                PrintNetworks(uscl);
                var newnetdelt = uscl.Network.Delete(newnet);
                PrintNetworks(uscl);
                newnetdelt.Wait();
                PrintNetworks(uscl);*/

                //uscl.Vpn.ChangePassword("dupa", "pHKzKxZyPx").Wait();

                //VirtualMachine nginxd = uscl.VirtualMachines.GetAllDetailed().Result.Where(x => x.name_tag == "nginxD").First();
                //uscl.VirtualMachines.PowerOff(nginxd).Wait();

                //VirtualMachine editable = uscl.VirtualMachines.GetAllDetailed().Result.Where(x => x.name_tag == "VAGa").First();
                /*Console.WriteLine("Creating 'JOBy' VM...");
                VirtualMachine editable = uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 1, memory_mb = 512, product_id = 19, virtual_machine_disks = new List<VirtualMachineDisk> { new VirtualMachineDisk { primary = true, storage_gb = 10, storage_type = VirtualMachineDiskStorageType.optimal } } }).Result;
                Console.WriteLine("Configuring 'JOBy' VM...");
                editable = uscl.VirtualMachines.Configure(editable, new VirtualMachineConfigurationPrototype { virtual_machine_name = "JOBy" }).Result;
                editable.cpu = 2;
                editable.memory_mb = 2048;
                editable.virtual_machine_disks.Add(new VirtualMachineDisk { primary = false, storage_gb = 100, storage_type = VirtualMachineDiskStorageType.optimal });
                editable.virtual_machine_disks.Add(new VirtualMachineDisk { primary = false, storage_gb = 250, storage_type = VirtualMachineDiskStorageType.optimal });
                Console.WriteLine("Changing CPU to 2 and MEM to 2GB + add 2 disks 'JOBy' VM...");
                editable = uscl.VirtualMachines.Edit(editable).Result;
                Console.WriteLine("EDIT COMPLETE 'JOBy' VM...");
                Thread.Sleep(3000);
                editable.virtual_machine_disks.RemoveAll(d => !d.primary);
                Console.WriteLine("Delete all additional disks 'JOBy' VM...");
                editable = uscl.VirtualMachines.Edit(editable).Result;
                Console.WriteLine("Delete 'JOBy' VM...");
                uscl.VirtualMachines.Delete(editable).Wait();*/

                //uscl.VirtualMachines.Rename(316, "LFLOW").Wait();


                /*List<VirtualMachine> test99 = new List<VirtualMachine>();
                while(true)
                {
                    try
                    {
                        test99.Add(uscl.VirtualMachines.Create(new VirtualMachinePrototype { cpu = 1, memory_mb = 1024, product_id = 19, virtual_machine_disks = new List<VirtualMachineDisk> { new VirtualMachineDisk { primary = true, storage_gb = 10, storage_type = VirtualMachineDiskStorageType.optimal } } }).Result);
                    }
                    catch (AggregateException e)
                    {
                        if (e.InnerException is ServerErrorException)
                            break;
                    }
                }

                List<Task<VirtualMachine>> tasks99 = new List<Task<VirtualMachine>>();
                foreach(var vm in test99)
                {
                    tasks99.Add(uscl.VirtualMachines.Configure(vm, new VirtualMachineConfigurationPrototype { virtual_machine_name = "TEST99n" + vm.id.ToString() }));
                }
                Task.WaitAll(tasks99.ToArray());

                Console.ReadKey();

                List<Task> delete99 = new List<Task>();
                foreach(var vm in test99)
                {
                    delete99.Add(uscl.VirtualMachines.Delete(vm));
                }
                Task.WaitAll(delete99.ToArray());*/

                /*var nginxA = uscl.VirtualMachines.GetAllDetailed().Result.Where(x => x.name_tag == "nginxA").First();
                var natnet = uscl.Network.GetNetworks().Result.First();
                var vmnat = uscl.Network.CreateNat(nginxA, natnet, "351", "352", NetworkProtocol.udp).Result;
                uscl.Network.DeleteNat(vmnat).Wait();*/

                Console.WriteLine("BILLING:");
                var billingsummary = uscl.Billing.GetOverview().Result;
                var costtimeline = uscl.Billing.GetCostTimeline().Result;
                Console.WriteLine("    Current cost: " + billingsummary.CurrentCost);
                Console.WriteLine("    Current balance: " + billingsummary.CurrentBalance);
                Console.WriteLine("    Current period: " + billingsummary.CurrentPeriod);
                Console.WriteLine("Cost Timeline:");
                Console.WriteLine("    Entries:");
                foreach (var entry in costtimeline.GetEntries())
                    Console.WriteLine(String.Format("        - {0} - {1} - {2} - {3}", entry.Date, entry.GetNumericCost(), entry.GetNumericComputingCost(), entry.GetNumericStorageCost()));
                Console.WriteLine("Credit Cards:");
                var ccs = uscl.Billing.GetCreditCards().Result;
                foreach (var cc in ccs)
                    Console.WriteLine(String.Format("    - {0} - {1} {2}", cc.CreditCardNumber, cc.CreditCardType, cc.IsDefault ? "*" : ""));
                /*uscl.Billing.AddCreditCard(
                    new BillingInformation
                    {
                        address = "beberebe",
                        city = "Wrocław",
                        company_name = "Świerszczyk",
                        country = "Albania",
                        first_name = "Zbychu",
                        last_name = "Zadychu",
                        phone_number = "09875",
                        state = "nostate",
                        tax_payer_id = "",
                        zip_code = "23232"
                    },
                    new CreditCardDetail
                    {
                        first_name = "Alfredar",
                        last_name = "Maj",
                        number = "4111 1111 1111 1111",
                        expiration_date = "0518",
                        code = "258"
                    }
                ).Wait();*/
                //ccs = uscl.Billing.GetCreditCards().Result;
                //uscl.Billing.MakeCreditCardDefault(ccs.First().is_default ? ccs.Last() : ccs.First()).Wait();
                //uscl.Billing.DeleteCreditCard(ccs.First().is_default ? ccs.First() : ccs.Last()).Wait();
                Console.WriteLine("Used Promo Codes:");
                var pcs = uscl.Billing.GetPromoCodes().Result;
                foreach (var pc in pcs)
                    Console.WriteLine(String.Format("    - {0} - {1} - {2}", pc.UsedAt, pc.Code, pc.Amount));
                //uscl.Billing.ApplyPromoCode("VAGRANT").Wait();

                var payments = uscl.Billing.GetPaymentHistory().Result;
                Console.WriteLine("Payments:");
                foreach (var payment in payments)
                    Console.WriteLine("    - " + payment.CreatedAt + " - " + (payment.PaymentMethod != null ? payment.PaymentMethod.DisplayName : "") + " - " + payment.Amount + " - " + payment.Status);


                var invoices = uscl.Billing.GetInvoices().Result;
                Console.WriteLine("Invoices:");
                foreach (var invoice in invoices)
                    Console.WriteLine(String.Format("    - {0} ({1} - {2}) - {3} - PAID: {4} (due: {5})  => {6}", invoice.IssuedAt, invoice.PeriodStart, invoice.PeriodEnd, invoice.Status, invoice.PaidAt, invoice.DueAt, invoice.Total));


                //var downloadedinvoice = uscl.Billing.DownloadInvoice(invoices.Last()).Result;
                //File.WriteAllBytes("C:/Users/publi/Desktop/lastfak.pdf", downloadedinvoice);

                var vmscosts = uscl.Billing.GetVirtualMachinesCosts().Result;
                Console.WriteLine("Current cost per vm:");
                foreach (var vmcost in vmscosts)
                    Console.WriteLine(String.Format("    - {0} - {1} CPU - {2} MEM - {3} DISK - Status: {4}   => {5}", vmcost.NameTag, vmcost.Cpu, vmcost.MemoryMb, vmcost.StorageGb, vmcost.Status, vmcost.Cost));

                var vmcosttimeline = uscl.Billing.GetVirtualMachineCostTimeline(315).Result;
                Console.WriteLine("VM 315 CONST TIMELINE:");
                foreach (var entry in vmcosttimeline.GetEntries())
                    Console.WriteLine(String.Format("        - {0} - {1} - {2} - {3}", entry.Date, entry.GetNumericCost(), entry.GetNumericComputingCost(), entry.GetNumericStorageCost()));

                var accountdetails = uscl.Account.GetAccountDetails().Result;
                Console.WriteLine("CURRENT USER: " + accountdetails.FirstName + " " + accountdetails.LastName + " (" + accountdetails.PhoneNumber + ", " + accountdetails.MobileNumber + ")");
                /*accountdetails.phone_number = "142536958";
                uscl.Account.UpdateAccountDetails(accountdetails).Wait();*/

                //uscl.Account.ChangePassword("***REMOVED***", "***REMOVED***").Wait();

                var sshkeys = uscl.Account.GetSshKeys().Result;
                Console.WriteLine("SSH KEYS:");
                foreach (var key in sshkeys)
                    Console.WriteLine(String.Format("    - {0} - {1} - {2} - {3}", key.Id, key.Name, key.Fingerprint, key.CreatedAt));

                uscl.Account.AddSshKey("KLUCZ", "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQCuJkbvCWyQE6gMXqRCcp/MFb0Ejw8ifoBI350Tub/wHFmXhYshj9615nQty/zu8BraTkRJw3+cn7R/Z02T5gXpn+j+23xcNyOZPDbOtCId5CTdBj0vPCezZvSDYG1uyzTWhEKtBTBrW/R2Vng7de39MZde5aWC08c5kmV9B7HDumgs51O4rnlpgwOlr2Ch2urlMqz5T6hPJlLxTgTE1SicBNzOR7QwwFmxQjE2KEsvPLPdKRs7KIBI5aKgPgm4bJMaQb/T8rlm+nrgnJ4gBidp5O1PHSYQIVNZpvBFVORTWLvxFzL9jlH5PLTRLB2Lk7AfjapJs72yPj18oGOLzyZh Rafal@DESKTOP").Wait();
                sshkeys = uscl.Account.GetSshKeys().Result;
                uscl.Account.DeleteSshKey(sshkeys.Last()).Wait();

                //PrepareLBTestClients();
            }
            catch (AggregateException e)
            {
                foreach (var ex in e.Flatten().InnerExceptions)
                    Console.WriteLine(ex);
            }

            /*seconduser = uscl.Users.AddUserPermission(seconduser, UserPermission.user_invite).Result;
            seconduser = uscl.Users.AddUserPermission(seconduser, UserPermission.user_update).Result;
            seconduser = uscl.Users.AddUserPermission(seconduser, UserPermission.user_delete).Result;
            seconduser = uscl.Users.RemoveUserPermission(seconduser, UserPermission.users_manage).Result;*/

            /*List<VirtualMachineSummary> vms = uscl.VirtualMachines.GetAll().Result;
            User usr = new User { id = 48 };
            List<VirtualMachinePermission> perms = new List<VirtualMachinePermission>()
            {
                VirtualMachinePermission.virtual_machine_view,
                VirtualMachinePermission.virtual_machine_power_on,
                VirtualMachinePermission.virtual_machine_suspend
            };
            foreach(var vm in vms)
            {
                uscl.VirtualMachines.RemovePermissionForUser(vm, usr, VirtualMachinePermission.virtual_machine_suspend).Wait();
            }*/

            Console.WriteLine("END END END");
            Console.ReadKey();
        }


        static void PrintNetworks(OneStepClient uscl)
        {
            List<PublicNetwork> pubnetworks = uscl.Network.GetNetworksDetailed().Result;
            Console.WriteLine("Public IPs:");
            foreach (var net in pubnetworks)
            {
                Console.WriteLine("    - " + net.Id.ToString() + ": " + net.IpAddress + " " + (net.Primary ? "primary" : "additional") + (net.Status != NetworkResourceStatus.idle ? " - " + net.Status.ToString() : ""));
                if (net.Nats.Count > 0)
                {
                    Console.WriteLine("        - NATS:");
                    foreach (var nat in net.Nats)
                        Console.WriteLine(String.Format("            - {0}({1}) - {5} - {4}    -    {2} => {3}", nat.VirtualMachineNameTag, nat.VirtualMachineId, nat.SourcePortRange, nat.DestinationPortRange, nat.Status.ToString(), nat.Protocol.ToString()));
                }
            }
        }

        static void StartOscOperations(OneStepClient cl)
        {
            var vm = cl.VirtualMachines.Get(326).Result;

            var cot = new Task<VirtualMachine>[2];
            var vms = new VirtualMachine[2];

            vms[0] = cl.VirtualMachines.Create(vm.GetPrototype()).Result;
            vms[1] = cl.VirtualMachines.Create(vm.GetPrototype()).Result;

            cot[0] = cl.VirtualMachines.Configure(vms[0], new VirtualMachineConfigurationPrototype { VirtualMachineName = "VVV" + 0 });
            cot[1] = cl.VirtualMachines.Configure(vms[1], new VirtualMachineConfigurationPrototype { VirtualMachineName = "VVV" + 1 });

            Task.WaitAll(cot);
            vms[0] = cot[0].Result.Id;
            vms[1] = cot[1].Result.Id;

            Console.WriteLine("VVV0: " + vms[0].id);
            Console.WriteLine("VVV1: " + vms[1].id);
        }

        static void PrepareLBTestClients()
        {
            OneStepClient cl = new OneStepClient(OneStepRegion.PL);
            cl.SignIn("firewatch", "***REMOVED***").Wait();

            //CLEAN ACCOUNT
            List<Task> cleantasks = new List<Task>();
            foreach (var vm in cl.VirtualMachines.GetAll().Result)
                cleantasks.Add(cl.VirtualMachines.Delete(vm));
            Task.WaitAll(cleantasks.ToArray());
            
            VirtualMachinePrototype vmproto = new VirtualMachinePrototype()
            {
                Cpu = 2,
                MemoryMb = 2048,
                ProductId = 4,
                VirtualMachineDisks = new List<VirtualMachineDisk>()
                {
                    new VirtualMachineDisk() {Primary= true, StorageGb = 10, StorageType = VirtualMachineDiskStorageType.optimal }
                }
            };

            VirtualMachine[] vms = new VirtualMachine[8];
            Task<VirtualMachine>[] creationJobs = new Task<VirtualMachine>[8];

            for (int i = 0; i < 8; i++)
            {
                vms[i] = cl.VirtualMachines.Create(vmproto).Result;
                Thread.Sleep(200);
            }


            for (int i = 0; i < 8; i++)
            {
                creationJobs[i] = cl.VirtualMachines.Configure(vms[i], new VirtualMachineConfigurationPrototype() { VirtualMachineName = "client5" + i });
                Thread.Sleep(300);
            }

            Task.WaitAll(creationJobs);
        }
    }
}
