using Newtonsoft.Json.Schema;
using OneStepCloudAPI.OneStepObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneStepCloudAPI.Samples.Code
{
    class Program
    {
        static string region;
        static string login;
        static string password;

        static OneStepClient oneStep;

        static void Main(string[] args)
        {
            if (args.Count() == 3)
            {
                region = args[0];
                login = args[1];
                password = args[2];
            }

            if (string.IsNullOrWhiteSpace(region))
            {
                Console.WriteLine("Enter region (US/PL):");
                region = Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(login))
            {
                Console.WriteLine("Enter username: ");
                login = Console.ReadLine();
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Enter password: ");
                password = Console.ReadLine();
            }

            if (region == "US")
                oneStep = new OneStepClient(OneStepRegion.US);
            else if (region == "PL")
                oneStep = new OneStepClient(OneStepRegion.PL);
            else
            {
                Console.WriteLine("REGION NOT KNOWN");
                return;
            }

            try
            {
                oneStep.PasswordSignIn(login, password).Wait();
                SessionSummary session = oneStep.SessionSummary().Result;

                var maintenancemessage = oneStep.MainenanceMessage().Result;
                if(!string.IsNullOrWhiteSpace(maintenancemessage))
                    Console.WriteLine("Maintenance: " + maintenancemessage);

                if (session.InvoiceAlert != null)
                {
                    Console.WriteLine("ALERT ALERT ALERT ALERT ALERT ALERT ALERT ALERT");
                    Console.WriteLine("      INVOICE UNPAID OR OVERDUE    ");
                    Console.WriteLine("ALERT ALERT ALERT ALERT ALERT ALERT ALERT ALERT");
                }

                if(session.AccountExpirationDate > 0)
                    Console.WriteLine($"!!!     Your account will expire at: {new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(session.AccountExpirationDate)}     !!!");

                if (session.AccountDeactivationDate > 0)
                    Console.WriteLine($"!!!     Your account will be deactivated at: {new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(session.AccountDeactivationDate)}     !!!");

                Console.WriteLine($"Current group status: {session.GroupStatus}");
                Console.WriteLine($"Current User Permissions ({session.Username}):");
                foreach (var perm in session.Permissions)
                    Console.WriteLine($"    - {perm.ToString()}");

                Console.WriteLine("Group resource usage summary:");
                GroupLimits limits = session.GroupLimits;
                Console.WriteLine($"{limits.Vms.Current}/{limits.Vms.Limit} VMs used");
                Console.WriteLine($"{limits.Dms.Current}/{limits.Dms.Limit} DMs used");
                Console.WriteLine($"{limits.Cpu.Current}/{limits.Cpu.Limit} CPUs used");
                Console.WriteLine($"{limits.MemoryMb.Current}/{limits.MemoryMb.Limit} MB RAM used");
                Console.WriteLine($"{limits.StorageGb.Current}/{limits.StorageGb.Limit} GB STORAGE used");
                Console.WriteLine($"{limits.PublicNetworks.Current}/{limits.PublicNetworks.Limit} IPs used");
                Console.WriteLine($"{limits.VirtualNetworks.Current}/{limits.VirtualNetworks.Limit} Virtual Networks used");

                if (session.ChackPermission(UserPermission.users_manage))
                {
                    Console.WriteLine("List of group users:");
                    List<User> users = oneStep.Users.GetAll().Result;
                    foreach (var user in users)
                        Console.WriteLine($"    - {user.Id} {user.Username} {user.Email}");

                    List<Invitation> invites = oneStep.Users.GetInvitations().Result;
                    if (invites.Count > 0)
                    {
                        Console.WriteLine("Sent user invitations:");
                        foreach (var i in invites)
                            Console.WriteLine($"    - {i.Id} {i.Username} {i.Email} Expires: {i.InvitationExpiresAt}");
                    }
                }

                var vms = oneStep.VirtualMachines.GetAllDetailed().Result;
                if (vms.Count > 0)
                {
                    Console.WriteLine("Virtual Machines:");
                    foreach (var vm in vms)
                    {
                        Console.WriteLine($"    - {vm.Id} - {vm.NameTag} - {vm.CreatedAt} - {vm.Username} - CPU: {vm.Cpu} - MEM: {vm.MemoryMb} - STORAGE: {vm.StorageGb} - {vm.PrivateNetworks.First().IpAddress}");
                        if (!string.IsNullOrEmpty(vm.Description))
                            Console.WriteLine($"        {vm.Description}");
                    }
                }

                if(session.ChackPermission(UserPermission.networking_manage))
                {
                    List<PublicNetwork> pubnetworks = oneStep.Network.GetPublicNetworksDetailed().Result;
                    Console.WriteLine("Public IPs:");
                    foreach (var net in pubnetworks)
                    {
                        Console.WriteLine("    - " + net.Id.ToString() + ": " + net.IpAddress + " " + (net.Primary ? "primary" : "additional") + (net.Status != NetworkResourceStatus.idle ? " - " + net.Status.ToString() : ""));
                        if (net.Nats.Count > 0)
                        {
                            Console.WriteLine("        - NATS:");
                            foreach (var nat in net.Nats)
                                Console.WriteLine($"            - {nat.MachineNameTag}({nat.MachineType.ToString()}: {nat.MachineId}) - {nat.Protocol.ToString()} - {nat.Status.ToString()}    -    {nat.SourcePortRange} => {nat.DestinationPortRange}");
                        }
                    }

                    List<VirtualNetwork> virnetworks = oneStep.Network.GetVritualNetworks().Result;
                    Console.WriteLine("Virtual networks:");
                    foreach(var net in virnetworks)
                    {
                        Console.WriteLine($"    - {net.Id}: {net.Name} - {net.Network} - External: {pubnetworks.First(x => x.Id == net.PublicNetworkId).IpAddress} - {(net.Primary ? "primary" : "additional")}");
                        if(net.PrivateNetworks.Count > 0)
                        {
                            Console.WriteLine("        - Private Networks:");
                            foreach (var priv in net.PrivateNetworks)
                                Console.WriteLine($"            - {priv.IpAddress} - {priv.Machine.Type} -> {priv.Machine.Name} ({priv.Machine.Id})");
                        }
                    }

                    List<PrivateNetwork> privnetworks = oneStep.Network.GetPrivateNetworks().Result;
                    Console.WriteLine("Private networks (adapters):");
                    foreach (var net in privnetworks)
                        Console.WriteLine($"    - {net.Id}: {net.IpAddress} - {net.Machine.Type} -> {net.Machine.Name} ({net.Machine.Id}) - Network: {net.VirtualNetwork.Name} - Outbound: {net.OutboundNetwork?.IpAddress ?? ("^" + pubnetworks.First(x => x.Id == virnetworks.First(y => y.Id == net.VirtualNetwork).PublicNetworkId).IpAddress) }");
                }

                if(session.ChackPermission(UserPermission.billing_manage))
                {
                    Console.WriteLine("BILLING:");
                    var billingsummary = oneStep.Billing.GetOverview().Result;
                    var additionalinvoiceitems = oneStep.Billing.GetCurrentInvoiceItems().Result;
                    var costtimeline = oneStep.Billing.GetCostTimeline().Result;
                    Console.WriteLine("    Current cost: " + billingsummary.CurrentCost);
                    Console.WriteLine($"        Computing cost: {billingsummary.ComputingCost} ({billingsummary.ComputingValue})");
                    Console.WriteLine($"        Storage cost: {billingsummary.StorageCost} ({billingsummary.StorageValue})");
                    Console.WriteLine($"        Network cost: {billingsummary.NetworkCost} ({billingsummary.NetworkValue})");
                    Console.WriteLine($"        Dedicated cost: {billingsummary.DedicatedCost} ({billingsummary.DedicatedValue})");
                    Console.WriteLine($"        Current additional costs: {billingsummary.OtherCost} ({billingsummary.OtherValue})");
                    Console.WriteLine("    Current balance: " + billingsummary.CurrentBalance);
                    Console.WriteLine("    Current period: " + billingsummary.PeriodStart.ToShortDateString() + " - " + billingsummary.PeriodEnd.ToShortDateString());
                    if (additionalinvoiceitems.Count > 0)
                    {
                        Console.WriteLine("Additional Current Invoice Items:");
                        foreach (var entry in additionalinvoiceitems)
                            Console.WriteLine($"    - {entry.Name} - {entry.Type} - {entry.Cost}");
                    }
                    if (costtimeline.Entries.Count > 0)
                    {
                        Console.WriteLine("Cost Timeline Entries:");
                        foreach (var entry in costtimeline.Entries)
                            Console.WriteLine($"    - {entry.Time} - +{entry.Value} - {entry.SumValue}");
                    }

                    var ccs = oneStep.Billing.GetCreditCards().Result;
                    if (ccs.Count > 0)
                    {
                        Console.WriteLine("Credit Cards:");
                        foreach (var cc in ccs)
                            Console.WriteLine($"    - {cc.CreditCardNumber} - {cc.CreditCardType} - {cc.CreatedAt} {(cc.IsDefault ? "*" : "")}");
                    }

                    var pcs = oneStep.Billing.GetPromoCodes().Result;
                    if (pcs.Count > 0)
                    {
                        Console.WriteLine("Used Promo Codes:");
                        foreach (var pc in pcs)
                            Console.WriteLine($"    - {pc.UsedAt} - {pc.Code} - {pc.Amount}");
                    }

                    var payments = oneStep.Billing.GetPaymentHistory().Result;
                    if (payments.Count > 0)
                    {
                        Console.WriteLine("Payments:");
                        foreach (var payment in payments)
                            Console.WriteLine("    - " + payment.CreatedAt.ToShortDateString() + " - " + (payment.PaymentMethod != null ? payment.PaymentMethod.DisplayName : "") + " - " + payment.Amount + " - " + payment.Status);
                    }

                    var usageReports = oneStep.Billing.GetUsageReports().Result;
                    if (usageReports.Count > 0)
                    {
                        Console.WriteLine("Usage reports:");
                        foreach (var report in usageReports)
                            Console.WriteLine($"    - {report.Id} - ({report.PeriodStart.ToShortDateString()} - {report.PeriodEnd.ToShortDateString()})");
                    }

                    var invoices = oneStep.Billing.GetInvoices().Result;
                    if (invoices.Count > 0)
                    {
                        Console.WriteLine("Invoices:");
                        foreach (var invoice in invoices)
                            Console.WriteLine($"    - {invoice.InvoiceNumber} - {invoice.IssuedAt.ToShortDateString()} ({invoice.PeriodStart.ToShortDateString()} - {invoice.PeriodEnd.ToShortDateString()}) - {invoice.Status} - PAID: {invoice.PaidAt.ToShortDateString()} (due: {invoice.DueAt.ToShortDateString()})  => {invoice.Total}");
                    }

                    var vmscosts = oneStep.Billing.GetVirtualMachinesCosts().Result;
                    if (vms.Count > 0 && vmscosts.Count > 0)
                    {
                        Console.WriteLine("Current cost per vm:");
                        foreach (var vmcost in vmscosts)
                            Console.WriteLine($"    - {vmcost.NameTag} - {vmcost.Cpu} CPU - {vmcost.MemoryMb} MEM - {vmcost.StorageGb} DISK - Status: {vmcost.Status}   => {vmcost.Cost}");

                        var vmcosttimeline = oneStep.Billing.GetVirtualMachineCostTimeline(vmscosts.First().Id).Result;
                        Console.WriteLine($"First Available VM Cost Timeline Entries (ID: {vmscosts.First().Id}):");
                        foreach (var entry in vmcosttimeline.Entries)
                            Console.WriteLine($"    - {entry.Time} - {entry.Computing + entry.Storage} SUM - {entry.Computing} Computing - {entry.Storage} Storage");
                    }
                }

                if(session.ChackPermission(UserPermission.marketplace_view))
                {
                    Console.WriteLine("MARKETPLACE:");
                    var marketplaces = oneStep.Marketplace.GetMarketplaceProducts().Result;
                    foreach (var mk in marketplaces)
                    {
                        Console.WriteLine($"    - {mk.Id} - {mk.Name}:");
                        foreach (var pr in mk.Items)
                            Console.WriteLine($"        - {pr.Id} - {pr.Name} - {pr.Type} - {pr.PaymentType} - {pr.Price}");
                    }

                    var marketlicenses = oneStep.Marketplace.GetPurchasedLicences().Result;
                    var marketsubscriptions = oneStep.Marketplace.GetPurchasedSubsctiptions().Result;
                    if (marketlicenses.Count > 0 || marketsubscriptions.Count > 0)
                    {
                        Console.WriteLine("MARKETPLACE PURCHASED PRODUCTS:");
                        if (marketlicenses.Count > 0)
                        {
                            Console.WriteLine("    - Licences:");
                            foreach (var li in marketlicenses)
                                Console.WriteLine($"        - {li.Id} - {li.ProductId} - {li.Name} - {li.Key} - {li.PurchasedAt} - {li.ExpiresAt} - {li.InvoiceId}");
                        }
                        if (marketsubscriptions.Count > 0)
                        {
                            Console.WriteLine("    - Subscriptions:");
                            foreach (var su in marketsubscriptions)
                                Console.WriteLine($"        - {su.Id} - {su.ProductId} - {su.Name} - {su.Active} - {su.PurchasedAt} - {su.ExpiresAt} - {su.InvoiceId} - {su.Expired}");
                        }
                    }
                }
                

                var accountdetails = oneStep.Account.GetAccountDetails().Result;
                Console.WriteLine("CURRENT USER:");
                Console.WriteLine($"    - Username: {session.Username}");
                if (!string.IsNullOrWhiteSpace(accountdetails.FirstName))
                    Console.WriteLine($"    - First Name: {accountdetails.FirstName}");
                if (!string.IsNullOrWhiteSpace(accountdetails.LastName))
                    Console.WriteLine($"    - Last Name: {accountdetails.LastName}");
                if (!string.IsNullOrWhiteSpace(accountdetails.PhoneNumber))
                    Console.WriteLine($"    - Phone Number: {accountdetails.PhoneNumber}");
                if (!string.IsNullOrWhiteSpace(accountdetails.MobileNumber))
                    Console.WriteLine($"    - Mobile Number: {accountdetails.MobileNumber}");
                if (!string.IsNullOrWhiteSpace(accountdetails.Language))
                    Console.WriteLine($"    - Portal Language: {accountdetails.Language}");

                var sshkeys = oneStep.Account.GetSshKeys().Result;
                if (sshkeys.Count > 0)
                {
                    Console.WriteLine("SSH KEYS:");
                    foreach (var key in sshkeys)
                        Console.WriteLine($"    - {key.Id} - {key.Name} - {key.Fingerprint} - {key.CreatedAt}");
                }

                Console.WriteLine("API PRICES:");
                var prices = oneStep.GetOSCPrices().Result;
                foreach (var price in prices)
                    Console.WriteLine("    - " + price.Key + " => " + price.Value);

                Console.WriteLine("API TEMPLATES:");
                var templates = oneStep.VirtualMachines.GetTemplates().Result;
                foreach (var t in templates)
                {
                    Console.WriteLine($"    - {t.Id} - {t.Name} - CPU({t.MinimumCpu}-{t.MaximumCpu}) - MEM({t.MinimumMemoryMb}-{t.MaximumMemoryMb}) - STORAGE({t.MaximumAdditionalDisks}*{t.MinimumStorageGb}-{t.MaximumStorageGb})");
                    Console.WriteLine($"        OS => {t.OperatingSystem.Id} - {t.OperatingSystem.Name} - {t.OperatingSystem.SystemType} - DISK: {t.OperatingSystem.StorageGb}");
                }

                Console.WriteLine("API PRODUCTS:");
                var products = oneStep.VirtualMachines.GetProducts().Result;
                foreach (var p in products)
                {
                    Console.WriteLine($"    - {p.Id} - {p.Name}");
                    foreach (var t in p.Products)
                    {
                        Console.WriteLine($"        - {t.Id} - {t.Name} - CPU({t.MinimumCpu}-{t.MaximumCpu}) - MEM({t.MinimumMemoryMb}-{t.MaximumMemoryMb}) - STORAGE({t.MaximumAdditionalDisks}*{t.MinimumStorageGb}-{t.MaximumStorageGb})");
                        Console.WriteLine($"            OS => {t.OperatingSystem.Id} - {t.OperatingSystem.Name} - {t.OperatingSystem.SystemType} - DISK: {t.OperatingSystem.StorageGb}");
                    }
                }
            }
            catch (AggregateException e)
            {
                foreach (var ex in e.Flatten().InnerExceptions)
                    Console.WriteLine(ex);
            }

            
            Console.WriteLine("\nPress any key to close application");
            Console.ReadKey();
        }
    }
}
