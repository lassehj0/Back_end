namespace Studieforeningskalender_Backend.Service.Helpers
{
	public static class EmailHelper
	{
		public static string GetForgotPasswordHTML(string firstName, string emailAddress, string validationToken, string baseUrl)
		{
			return $@"
				<!DOCTYPE html>
				<html lang=""en"">
					<head>
						<meta charset=""UTF-8"" />
						<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
						<title>Password Reset Verification</title>
					</head>
					<body>
						<div style=""font-family: Arial, sans-serif; color: #333; padding: 20px"">
							<h2>Verify Email Address</h2>
							<p>Hello {firstName},</p>
							<p>
								You recently requested to reset your password for your account. Use the verification code below to complete the process:
							</p>

							<div
								style=""
								position: relative;
								user-select: none;
								cursor: pointer;
								background-color: #2056a8;
								border: 0.125rem solid #2056a8;
								border-radius: 0.25rem;
								text-align: center;
								outline: none;
								transition: all 0.4s;
								width: fit-content;
								height: fit-content;
								font-size: 1.5rem;
								padding: 0.5rem 1rem;
								left: 50%;
								transform: translateX(-50%);
								""
							>
								<a
									href=""{baseUrl}/Login?email={emailAddress}&token={validationToken}""
									target=""_blank""
									style=""color: white; text-decoration: none""
								>
									Verify Email Address
								</a>
							</div>

							<p style=""margin-top: 20px; text-wrap: wrap; overflow-wrap: break-word"">
								If you did not request a password reset, no further action is required on your part.
							</p>

							<p style=""margin-top: 20px; text-wrap: wrap; overflow-wrap: break-word"">
								Having trouble? If clicking the button does not redirect you simply copy the text below and put it into the search bar: 
								<br />
								{baseUrl}/Login?email={emailAddress}&token={validationToken}
							</p>
						</div>
					</body>
				</html>";
		}

		public static string GetVerifyRegistrationHTML(string firstName, string emailAddress, string validationToken, string baseUrl)
		{
			return $@"
				<!DOCTYPE html>
				<html lang=""en"">
					<head>
						<meta charset=""UTF-8"" />
						<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
						<title>Register User Verification</title>
					</head>
					<body>
						<div style=""font-family: Arial, sans-serif; color: #333; padding: 20px"">
							<h2>Verify Email Address</h2>
							<p>Hello {firstName},</p>
							<p>
								You recently tried to register for studieforeningskalenderen.com. Use the verification code below to complete the process:
							</p>

							<div
								style=""
								position: relative;
								user-select: none;
								cursor: pointer;
								background-color: #2056a8;
								border: 0.125rem solid #2056a8;
								border-radius: 0.25rem;
								text-align: center;
								outline: none;
								transition: all 0.4s;
								width: fit-content;
								height: fit-content;
								font-size: 1.5rem;
								padding: 0.5rem 1rem;
								left: 50%;
								transform: translateX(-50%);
								""
							>
								<a 
									href=""{baseUrl}/Verify?email={emailAddress}&token={validationToken}"" 
									target=""_blank""
									style=""color: white; text-decoration: none""
								>
									Verify Email Address
								</a>
							</div>

							<p style=""margin-top: 20px; text-wrap: wrap; overflow-wrap: break-word"">
								If you did not try to register, no further action is required on your part.
							</p>

							<p style=""margin-top: 20px; text-wrap: wrap; overflow-wrap: break-word"">
								Having trouble? If clicking the button does not redirect you simply copy the text below and put it into the search bar: 
								<br />
								{baseUrl}/Verify?email={emailAddress}&token={validationToken}
							</p>
						</div>
					</body>
				</html>";
		}
	}
}
