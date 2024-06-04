using FluentValidation;
using SixLabors.ImageSharp;

namespace Studieforeningskalender_Backend.Service.Helpers
{
	public static class ValidationHelper
	{
		public static IRuleBuilderOptions<T, IFile> MustHaveDimensions<T>(this IRuleBuilder<T, IFile> ruleBuilder)
		{
			return ruleBuilder.MustAsync(async (rootObject, file, context, cancellationToken) =>
			{
				try
				{
					using (var stream = file.OpenReadStream())
					{
						var image = await Image.LoadAsync(stream, cancellationToken);
						return image.Width >= 640 && image.Height >= 360;
					}
				}
				catch
				{
					return false;
				}
			})
			.WithMessage("Image must have a width of minimum 640 pixels and a height of minimum 360 pixels.");
		}

		public static IRuleBuilderOptions<T, IFile> MustHaveRatio<T>(this IRuleBuilder<T, IFile> ruleBuilder)
		{
			return ruleBuilder.MustAsync(async (rootObject, file, context, cancellationToken) =>
			{
				try
				{
					using (var stream = file.OpenReadStream())
					{
						var image = await Image.LoadAsync(stream, cancellationToken);
						var imageRatio = (double)image.Width / image.Height;
						return Math.Abs(imageRatio - (16.0 / 9.0)) < 0.01;
					}
				}
				catch (IOException ex)
				{
					context.AddFailure($"Error opening image file: {ex.Message}");
					return false;
				}
				catch (Exception ex)
				{
					context.AddFailure($"Error loading image: {ex.Message}");
					return false;
				}
			})
			.WithMessage("Image must have a 16:9 ratio.");
		}
	}
}
