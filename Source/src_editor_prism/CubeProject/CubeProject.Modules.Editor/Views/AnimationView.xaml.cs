using System;
using System.Windows;
using System.Windows.Controls;
using CubeProject.Infrastructure.UI;
using CubeProject.Modules.Editor.ViewModels;

namespace CubeProject.Modules.Editor.Views
{
    /// <summary>
    /// Interaction logic for AnimationView.xaml
    /// </summary>
    public partial class AnimationView : UserControl
    {
        ListViewDragDropManager<FrameViewModel> dragMgr;
        public AnimationView()
        {
            InitializeComponent();
            AnimListView.DragEnter += AnimListView_DragEnter;
            AnimListView.AllowDrop = true;

            this.DataContextChanged += AnimationView_DataContextChanged;
        }

        void AnimationView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.dragMgr = new ListViewDragDropManager<FrameViewModel>(AnimListView);
            this.dragMgr.ProcessDrop += dragMgr_ProcessDrop;
        }

        void dragMgr_ProcessDrop(object sender, ProcessDropEventArgs<FrameViewModel> e)
        {
            // This shows how to customize the behavior of a drop.
            // Here we perform a swap, instead of just moving the dropped item.

            int higherIdx = Math.Max(e.OldIndex, e.NewIndex);
            int lowerIdx = Math.Min(e.OldIndex, e.NewIndex);


            // null values will cause an error when calling Move.
            // It looks like a bug in ObservableCollection to me.
            if (e.ItemsSource[lowerIdx] == null ||
                e.ItemsSource[higherIdx] == null)
                return;

            // The item came from the ListView into which
            // it was dropped, so swap it with the item
            // at the target index.
            e.ItemsSource.Move(lowerIdx, higherIdx);
            e.ItemsSource.Move(higherIdx - 1, lowerIdx);

            // Set this to 'Move' so that the OnListViewDrop knows to 
            // remove the item from the other ListView.
            e.Effects = DragDropEffects.Move;
        }

        void AnimListView_DragEnter(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }
    }
}
