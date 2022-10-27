using System;

public class CarPark
{
    class ParkingSpace
    {
        public int space_ID;
        public int space_size;
        public bool space_taken;

        public ParkingSpace(int Space_ID, int Space_size, bool Space_taken)
        {
            space_ID = Space_ID;
            space_size = Space_size;
            space_taken = Space_taken;
        }
    }
    class Ticket : ParkingSpace
    {
        public int ticket_ID;
        public int vehicle_size;
        public DateTime date_issued;
        public DateTime date_returned;

        public Ticket(int Ticket_ID, int Space_ID, int Vehicle_size, DateTime Date_issued, DateTime Date_returned) : base(0, 0, true)
        {
            ticket_ID = Ticket_ID;
            space_ID = Space_ID;
            vehicle_size = Vehicle_size;
            date_issued = Date_issued;
            date_returned = Date_returned;
        }
    }

    public static void Main()
    {
        List<Ticket> ticket_list = new();
        List<ParkingSpace> parking_space = new();

        // Populate lists with testing data (tests for adequate size first, then larger)
        parking_space.Add(new ParkingSpace(1, 1, true));
        parking_space.Add(new ParkingSpace(2, 1, true));
        parking_space.Add(new ParkingSpace(3, 2, false));
        parking_space.Add(new ParkingSpace(4, 2, true));
        parking_space.Add(new ParkingSpace(5, 3, false));
        parking_space.Add(new ParkingSpace(6, 3, true));

        ticket_list.Add(new Ticket(12, 1, 1, DateTime.Now, DateTime.Now));
        ticket_list.Add(new Ticket(13, 2, 4, DateTime.Now, DateTime.Now));
        ticket_list.Add(new Ticket(14, 3, 6, DateTime.Now, DateTime.Now));

        // This information would be obtained from a sensor. Change for testing
        int vehicle_size = 1;
        int available_space;
        bool exit = false;

        Console.WriteLine("Testing: Current vehicle size = {0}, change line 53 for different results.", vehicle_size);


        void FindUserByEmail(List<userInfo>, var emailAddress)
        {
            if (userInfo.Contains(emailAddress))
            {
                var requestedInfo = userInfo.Where(x => x.emailAddress == emailAddress).Select(x => x.firstName, x.lastName);
            }

        }

        void AssignSpace(int available_space)
        {
            // Mark space as taken
            parking_space.Where(item => item.space_ID == available_space).ToList().ForEach(item => item.space_taken = true);

            // Create a unique ID for the ticket
            Random rnd = new();
            int ticket_ID = rnd.Next(1, 50000);
            ticket_list.Add(new Ticket(ticket_ID, available_space, vehicle_size, DateTime.Now, DateTime.Now));

            // Print out the ticket
            Console.WriteLine("Your assigned space is: {0}. Your ticket number: {1}", available_space, ticket_ID);
        }

        do
        {
            Console.WriteLine("\nWelcome to the Parking Lot Company! What would you like to do?");
            Console.WriteLine("New ticket: Select 1, Retrieve ticket: Select 2, Exit: 3");
            int answer = Convert.ToInt32(Console.ReadLine());

            // Create ticket and update lists
            if (answer == 1)
            {
                try
                {
                    // Check if a space of the vehicle's size is available
                    if (parking_space.Where(item => item.space_taken == false && item.space_size == vehicle_size).FirstOrDefault() == null)
                    {
                        do
                        {
                            // Check if a suitable parking space exists, break if no more spaces left
                            if (parking_space.Where(item => item.space_taken == false && item.space_size >= vehicle_size).FirstOrDefault() == null)
                            {
                                Console.WriteLine("Sorry, no suitable space has been found.");
                                break;
                            }
                            // Assign the parking space
                            else
                            {
                                vehicle_size++;
                                available_space = parking_space.Where(item => item.space_taken == false && item.space_size >= vehicle_size).FirstOrDefault().space_ID;
                                AssignSpace(available_space);
                                break;
                            }
                        }
                        while (vehicle_size < 3);
                    }
                    else
                    {
                        // Mark parking space as taken in the parking list
                        available_space = parking_space.Where(item => item.space_taken == false && item.space_size == vehicle_size).FirstOrDefault().space_ID;
                        AssignSpace(available_space);
                    }

                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("There was a problem with generating a ticket, please contact administration. Error code: " + e);
                    Console.ReadLine();
                }
            }
            // Retrieve a ticket
            else if (answer == 2)
            {
                try
                {
                    // This would be handled by a scanner, no manual input allowed to minimise exceptions
                    Console.WriteLine("Please enter your ticket number: ");
                    int ticket_no = Convert.ToInt32(Console.ReadLine());

                    // Check if such ticket exists in the database and update space as free
                    if (ticket_list.Where(item => item.ticket_ID == ticket_no).FirstOrDefault() != null)
                    {
                        // Mark the time of leaving the car park on the ticket
                        ticket_list.Where(item => item.ticket_ID == ticket_no).ToList().ForEach(item => item.date_returned = DateTime.Now);
                        Console.WriteLine("Ticket recognised. Thank you for using our parking lot. We hope that your journey went smoothly.");
                    }
                    else
                    {
                        Console.WriteLine("\nSuch ticket does not exist, please contact administration.");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while retrieving the ticket, error code: " + e);
                }
            }
            else if (answer == 3)
            {
                exit = true;
            }
            else
            {
                Console.WriteLine("Input not recognized, please try again.");
            }
        } while (exit == false);
    }

}



