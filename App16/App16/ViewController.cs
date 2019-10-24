using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace App16
{

    public partial class ViewController : UIViewController
    {
        private UICollectionView _collectionView;

        public static List<BaseModel> Models = new List<BaseModel>();


        public ViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            PopulateModels();

            _collectionView = new UICollectionView(View.Frame, GetUiCollectionViewLayout())
            {
                BackgroundColor = UIColor.Brown,
                DataSource = new MyViewUICollectionViewDataSource(),
                ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never,
                TranslatesAutoresizingMaskIntoConstraints = false,
                ShowsVerticalScrollIndicator = false,
                ShowsHorizontalScrollIndicator = false
            };

            _collectionView.RegisterClassForCell(typeof(MyViewUICollectionViewCell), MyViewUICollectionViewCell.ReuseIdentifier);
            _collectionView.RegisterClassForCell(typeof(MyViewUICollectionViewCell2), MyViewUICollectionViewCell2.ReuseIdentifier);

            View.AddSubview(_collectionView);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
            _collectionView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor),
            _collectionView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor),
            _collectionView.LeftAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeftAnchor),
            _collectionView.RightAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.RightAnchor)
        });
        }

        private static void PopulateModels()
        {
            string a = "testLongTexttestLongTexttestLongText";

            for (var i = 0; i < 100; i++)
            {
                BaseModel baseModel;

                a += "testLongTexttestLongTexttestLongText";

                if (i % 2 == 0)
                {
                    baseModel = new Model1
                    {
                        UIColor = UIColor.Red
                    };
                }
                else
                {
                    baseModel = new Model2
                    {
                        Text = a
                    };         
                }   
                Models.Add(baseModel);
            }
        }

        private UICollectionViewLayout GetUiCollectionViewLayout()
        {
            var layoutSize = NSCollectionLayoutSize.Create(NSCollectionLayoutDimension.CreateFractionalWidth(1), NSCollectionLayoutDimension.CreateEstimated(100));
            var item = NSCollectionLayoutItem.Create(layoutSize);
            var group = NSCollectionLayoutGroup.CreateHorizontal(layoutSize: layoutSize, subitem: item, count: 1);
            var section = NSCollectionLayoutSection.Create(group);

            // this is what you need for content inset
            section.ContentInsets = new NSDirectionalEdgeInsets(top: 5, leading: 5, bottom: 5, trailing: 5);

            // this is spacing between items
            section.InterGroupSpacing = 5;

            var layout = new UICollectionViewCompositionalLayout(section);

            return layout;
        }
    }

    public class BaseModel
    {

    }

    public class Model1 : BaseModel
    {
        public UIColor UIColor { get; set; }
    }

    public class Model2 : BaseModel
    {
        public string Text { get; set; }
    }

    public class MyViewUICollectionViewDataSource : UICollectionViewDataSource
    {
        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var model = ViewController.Models[(int)indexPath.Item];

            switch (model)
            {
                case Model1 model1:
                    {
                        var cell = collectionView.DequeueReusableCell(MyViewUICollectionViewCell.ReuseIdentifier, indexPath) as MyViewUICollectionViewCell;
                        cell.ColorView.BackgroundColor = model1.UIColor;
                        return cell;
                    }
                case Model2 model2:
                    {
                        var cell2 = collectionView.DequeueReusableCell(MyViewUICollectionViewCell2.ReuseIdentifier, indexPath) as MyViewUICollectionViewCell2;
                        cell2.Label.Text = model2.Text;
                        return cell2;
                    }
                default:
                    throw new Exception();
            }
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return ViewController.Models.Count;
        }
    }

    public class MyViewUICollectionViewCell : UICollectionViewCell
    {
        public const string ReuseIdentifier = "MyViewUICollectionViewCell";

        public UIView ColorView { get; }

        [Export("initWithFrame:")]
        public MyViewUICollectionViewCell(CGRect frame) : base(frame)
        {
            var container = new UIView
            {
                BackgroundColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            container.Layer.CornerRadius = 5;

            ContentView.AddSubview(container);

            container.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            container.LeftAnchor.ConstraintEqualTo(ContentView.LeftAnchor).Active = true;
            container.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;
            container.RightAnchor.ConstraintEqualTo(ContentView.RightAnchor).Active = true;

            ColorView = new UIView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            ColorView.Layer.CornerRadius = 10;

            container.AddSubview(ColorView);

            ColorView.CenterXAnchor.ConstraintEqualTo(container.CenterXAnchor).Active = true;
            ColorView.CenterYAnchor.ConstraintEqualTo(container.CenterYAnchor).Active = true;
            ColorView.WidthAnchor.ConstraintEqualTo(20).Active = true;
            ColorView.HeightAnchor.ConstraintEqualTo(50).Active = true;
        }
    }

    public class MyViewUICollectionViewCell2 : UICollectionViewCell
    {
        public const string ReuseIdentifier = "MyViewUICollectionViewCell2";

        public UILabel Label { get; }

        [Export("initWithFrame:")]
        public MyViewUICollectionViewCell2(CGRect frame) : base(frame)
        {
            var container = new UIView
            {
                BackgroundColor = UIColor.White,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            container.Layer.CornerRadius = 5;

            ContentView.AddSubview(container);

            container.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor).Active = true;
            container.LeftAnchor.ConstraintEqualTo(ContentView.LeftAnchor).Active = true;
            container.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor).Active = true;
            container.RightAnchor.ConstraintEqualTo(ContentView.RightAnchor).Active = true;

            Label = new UILabel
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };


            Label.Lines = 0;
            container.AddSubview(Label);

            Label.TopAnchor.ConstraintEqualTo(container.TopAnchor).Active = true;
            Label.LeftAnchor.ConstraintEqualTo(container.LeftAnchor).Active = true;
            Label.BottomAnchor.ConstraintEqualTo(container.BottomAnchor).Active = true;
            Label.RightAnchor.ConstraintEqualTo(container.RightAnchor).Active = true;

            //Label.CenterXAnchor.ConstraintEqualTo(container.CenterXAnchor).Active = true;
            //CenterYAnchor.ConstraintEqualTo(container.CenterYAnchor).Active = true;
            //Label.WidthAnchor.ConstraintEqualTo(20).Active = true;
            //Label.HeightAnchor.ConstraintEqualTo(20).Active = true;
        }
    }

}