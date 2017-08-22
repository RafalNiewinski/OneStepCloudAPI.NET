using Newtonsoft.Json.Schema;
using OneStepCloudAPI.OneStepObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Samples.Net452
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

                SessionSummary ss = uscl.SessionSummary().Result;

                //ALERT
                if (ss.InvoiceAlert != null)
                {
                    Console.WriteLine("ALERT ALERT ALERT ALERT ALERT ALERT ALERT ALERT");
                    Console.WriteLine("      INVOICE UNPAID OR OVERDUE    ");
                    Console.WriteLine("ALERT ALERT ALERT ALERT ALERT ALERT ALERT ALERT");
                }

                Console.WriteLine($"Current User Permissions ({ss.Username}):");
                foreach (var perm in ss.Permissions)
                    Console.WriteLine($"    - {perm.ToString()}");

                GroupLimits limits = ss.GroupLimits;
                Console.WriteLine($"Summary vms: {limits.Vms.Current}/{limits.Vms.Limit}");

                Console.WriteLine("Maintenance: " + uscl.MainenanceMessage().Result);

                Console.WriteLine("Virtual Machines:");
                var vms = uscl.VirtualMachines.GetAllDetailed().Result;
                foreach (var vm in vms)
                    Console.WriteLine($"    - {vm.Id} - {vm.NameTag} - {vm.CreatedAt} - {vm.Username} - CPU: {vm.Cpu} - MEM: {vm.MemoryMb} - STORAGE: {vm.StorageGb} - {vm.PrivateNetworks.First().IpAddress}");

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
                        var template = uscl.VirtualMachines.GetTemplates().Result.Where(x => x.Name == "windows_server_2012_r2").First();
                        test99.Add(uscl.VirtualMachines.Get(uscl.VirtualMachines.Create(template.GetVirtualMachinePrototype(5, 1024, VirtualMachineDiskStorageType.optimal, new List<VirtualMachineDisk> { new VirtualMachineDisk { Primary = true, StorageGb = 10, StorageType = VirtualMachineDiskStorageType.performance } })).Result).Result);
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
                    tasks99.Add(uscl.VirtualMachines.Configure(vm, new VirtualMachineConfigurationPrototype { VirtualMachineName = "TEST99n" + vm.Id.ToString() }));
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
                var additionalinvoiceitems = uscl.Billing.GetCurrentInvoiceItems().Result;
                var costtimeline = uscl.Billing.GetCostTimeline().Result;
                Console.WriteLine("    Current cost: " + billingsummary.CurrentCost);
                Console.WriteLine($"        Computing cost: {billingsummary.ComputingCost} ({billingsummary.ComputingValue})");
                Console.WriteLine($"        Storage cost: {billingsummary.StorageCost} ({billingsummary.StorageValue})");
                Console.WriteLine($"        Network cost: {billingsummary.NetworkCost} ({billingsummary.NetworkValue})");
                Console.WriteLine($"        Current additional costs: {billingsummary.OtherCost} ({billingsummary.OtherValue})");
                Console.WriteLine("    Current balance: " + billingsummary.CurrentBalance);
                Console.WriteLine("    Current period: " + billingsummary.PeriodStart.ToShortDateString() + " - " + billingsummary.PeriodEnd.ToShortDateString());
                Console.WriteLine("Additional Current Invoice Items:");
                foreach (var entry in additionalinvoiceitems)
                    Console.WriteLine($"    - {entry.Name} - {entry.Type} - {entry.Cost}");
                Console.WriteLine("Cost Timeline:");
                Console.WriteLine("    Entries:");
                foreach (var entry in costtimeline.GetEntries())
                    Console.WriteLine($"        - {entry.Date} - {entry.GetNumericCost()} - {entry.GetNumericComputingCost()} - {entry.GetNumericStorageCost()}");
                Console.WriteLine("Credit Cards:");
                var ccs = uscl.Billing.GetCreditCards().Result;
                foreach (var cc in ccs)
                    Console.WriteLine($"    - {cc.CreditCardNumber} - {cc.CreditCardType} - {cc.CreatedAt} {(cc.IsDefault ? "*" : "")}");
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
                    Console.WriteLine($"    - {pc.UsedAt} - {pc.Code} - {pc.Amount}");
                //uscl.Billing.ApplyPromoCode("VAGRANT").Wait();

                var payments = uscl.Billing.GetPaymentHistory().Result;
                Console.WriteLine("Payments:");
                foreach (var payment in payments)
                    Console.WriteLine("    - " + payment.CreatedAt.ToShortDateString() + " - " + (payment.PaymentMethod != null ? payment.PaymentMethod.DisplayName : "") + " - " + payment.Amount + " - " + payment.Status);


                var invoices = uscl.Billing.GetInvoices().Result;
                Console.WriteLine("Invoices:");
                foreach (var invoice in invoices)
                    Console.WriteLine($"    - {invoice.InvoiceNumber} - {invoice.IssuedAt.ToShortDateString()} ({invoice.PeriodStart.ToShortDateString()} - {invoice.PeriodEnd.ToShortDateString()}) - {invoice.Status} - PAID: {invoice.PaidAt.ToShortDateString()} (due: {invoice.DueAt.ToShortDateString()})  => {invoice.Total}");

                Console.WriteLine("Trying to pay all unpaid invoices");
                foreach (var invoice in invoices.Where(i => i.Status == InvoiceStatus.unpaid))
                    uscl.Billing.PayInvoice(invoice).Wait();


                //var downloadedinvoice = uscl.Billing.DownloadInvoice(invoices.Last()).Result;
                //File.WriteAllBytes("C:/Users/publi/Desktop/lastfak.pdf", downloadedinvoice);

                var vmscosts = uscl.Billing.GetVirtualMachinesCosts().Result;
                Console.WriteLine("Current cost per vm:");
                foreach (var vmcost in vmscosts)
                    Console.WriteLine($"    - {vmcost.NameTag} - {vmcost.Cpu} CPU - {vmcost.MemoryMb} MEM - {vmcost.StorageGb} DISK - Status: {vmcost.Status}   => {vmcost.Cost}");

                var vmcosttimeline = uscl.Billing.GetVirtualMachineCostTimeline(315).Result;
                Console.WriteLine("VM 315 CONST TIMELINE:");
                foreach (var entry in vmcosttimeline.GetEntries())
                    Console.WriteLine($"        - {entry.Date} - {entry.GetNumericCost()} - {entry.GetNumericComputingCost()} - {entry.GetNumericStorageCost()}");

                Console.WriteLine("MARKETPLACE:");
                var marketplaces = uscl.Marketplace.GetMarketplaceProducts().Result;
                foreach (var mk in marketplaces)
                {
                    Console.WriteLine($"    - {mk.Id} - {mk.Name}:");
                    foreach (var pr in mk.Items)
                        Console.WriteLine($"        - {pr.Id} - {pr.Name} - {pr.Type} - {pr.PaymentType} - {pr.Price}");
                }

                Console.WriteLine("MARKETPLACE PURCHASED PRODUCTS:");
                var marketlicenses = uscl.Marketplace.GetPurchasedLicences().Result;
                var marketsubscriptions = uscl.Marketplace.GetPurchasedSubsctiptions().Result;
                Console.WriteLine("    - Licences:");
                foreach (var li in marketlicenses)
                    Console.WriteLine($"        - {li.Id} - {li.ProductId} - {li.Name} - {li.Key} - {li.PurchasedAt} - {li.ExpiresAt} - {li.InvoiceId}");
                Console.WriteLine("    - Subscriptions:");
                foreach (var su in marketsubscriptions)
                    Console.WriteLine($"        - {su.Id} - {su.ProductId} - {su.Name} - {su.Active} - {su.PurchasedAt} - {su.ExpiresAt} - {su.InvoiceId} - {su.Expired}");

                var accountdetails = uscl.Account.GetAccountDetails().Result;
                Console.WriteLine("CURRENT USER: " + accountdetails.FirstName + " " + accountdetails.LastName + " (" + accountdetails.PhoneNumber + ", " + accountdetails.MobileNumber + ")");
                /*accountdetails.phone_number = "142536958";
                uscl.Account.UpdateAccountDetails(accountdetails).Wait();*/

                //uscl.Account.ChangePassword("***REMOVED***", "***REMOVED***").Wait();

                var sshkeys = uscl.Account.GetSshKeys().Result;
                Console.WriteLine("SSH KEYS:");
                foreach (var key in sshkeys)
                    Console.WriteLine($"    - {key.Id} - {key.Name} - {key.Fingerprint} - {key.CreatedAt}");

                uscl.Account.AddSshKey("KLUCZ", "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQCuJkbvCWyQE6gMXqRCcp/MFb0Ejw8ifoBI350Tub/wHFmXhYshj9615nQty/zu8BraTkRJw3+cn7R/Z02T5gXpn+j+23xcNyOZPDbOtCId5CTdBj0vPCezZvSDYG1uyzTWhEKtBTBrW/R2Vng7de39MZde5aWC08c5kmV9B7HDumgs51O4rnlpgwOlr2Ch2urlMqz5T6hPJlLxTgTE1SicBNzOR7QwwFmxQjE2KEsvPLPdKRs7KIBI5aKgPgm4bJMaQb/T8rlm+nrgnJ4gBidp5O1PHSYQIVNZpvBFVORTWLvxFzL9jlH5PLTRLB2Lk7AfjapJs72yPj18oGOLzyZh Rafal@DESKTOP").Wait();
                sshkeys = uscl.Account.GetSshKeys().Result;
                uscl.Account.DeleteSshKey(sshkeys.Last()).Wait();

                Console.WriteLine("PRICES:");
                var prices = uscl.GetOSCPrices().Result;
                foreach (var price in prices)
                    Console.WriteLine("    - " + price.Key + " => " + price.Value);

                Console.WriteLine("TEMPLATES:");
                var templates = uscl.VirtualMachines.GetTemplates().Result;
                foreach (var t in templates)
                {
                    Console.WriteLine($"    - {t.Id} - {t.Name} - CPU({t.MinimumCpu}-{t.MaximumCpu}) - MEM({t.MinimumMemoryMb}-{t.MaximumMemoryMb}) - STORAGE({t.MaximumAdditionalDisks}*{t.MinimumStorageGb}-{t.MaximumStorageGb})");
                    Console.WriteLine($"        OS => {t.OperatingSystem.Id} - {t.OperatingSystem.Name} - {t.OperatingSystem.SystemType} - DISK: {t.OperatingSystem.StorageGb}");
                }

                Console.WriteLine("PRODUCTS:");
                var products = uscl.VirtualMachines.GetProducts().Result;
                foreach (var p in products)
                {
                    Console.WriteLine($"    - {p.Id} - {p.Name}");
                    foreach (var t in p.Products)
                    {
                        Console.WriteLine($"        - {t.Id} - {t.Name} - CPU({t.MinimumCpu}-{t.MaximumCpu}) - MEM({t.MinimumMemoryMb}-{t.MaximumMemoryMb}) - STORAGE({t.MaximumAdditionalDisks}*{t.MinimumStorageGb}-{t.MaximumStorageGb})");
                        Console.WriteLine($"            OS => {t.OperatingSystem.Id} - {t.OperatingSystem.Name} - {t.OperatingSystem.SystemType} - DISK: {t.OperatingSystem.StorageGb}");
                    }
                }


                Console.WriteLine("CONFIGURATION SCHEMA (HAPROXY):");
                var dokonfigu = uscl.VirtualMachines.Get(781).Result;
                var config = uscl.VirtualMachines.GetConfigurationSchema(dokonfigu).Result;
                foreach (var prop in config.Properties)
                {
                    Console.WriteLine("    - " + prop.Key + " - " + prop.Value.Type + (prop.Value.Pattern != null ? $" ({prop.Value.Pattern})" : ""));
                    if (prop.Value.Type == JSchemaType.Array && prop.Value.Items.Count > 0)
                    {
                        foreach (var item in prop.Value.Items)
                        {
                            Console.WriteLine("        - " + item.Title + " - " + item.Type);
                            foreach (var iprop in item.Properties)
                                Console.WriteLine("            - " + iprop.Key + " - " + iprop.Value.Type + (iprop.Value.Pattern != null ? $" ({iprop.Value.Pattern})" : ""));
                        }
                    }
                }

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
                        Console.WriteLine($"            - {nat.VirtualMachineNameTag}({nat.VirtualMachineId}) - {nat.Protocol.ToString()} - {nat.Status.ToString()}    -    {nat.SourcePortRange} => {nat.DestinationPortRange}");
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

            cot[0] = cl.VirtualMachines.Configure(vms[0], new { VirtualMachineName = "VVV" + 0 });
            cot[1] = cl.VirtualMachines.Configure(vms[1], new { VirtualMachineName = "VVV" + 1 });

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
                creationJobs[i] = cl.VirtualMachines.Configure(vms[i], new { VirtualMachineName = "client5" + i });
                Thread.Sleep(300);
            }

            Task.WaitAll(creationJobs);
        }
    }
}
